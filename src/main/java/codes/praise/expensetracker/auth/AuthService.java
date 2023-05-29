package codes.praise.expensetracker.auth;

import codes.praise.expensetracker.role.Role;
import codes.praise.expensetracker.security.jwt.JwtService;
import codes.praise.expensetracker.security.jwt.UserDetailsService;
import codes.praise.expensetracker.token.Token;
import codes.praise.expensetracker.token.TokenService;
import codes.praise.expensetracker.token.TokenType;
import codes.praise.expensetracker.user.User;
import codes.praise.expensetracker.user.UserService;
import lombok.RequiredArgsConstructor;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

import java.util.List;

@Service
@RequiredArgsConstructor
public class AuthService {

    private final UserService userService;
    private final UserDetailsService userDetailsService;
    private final JwtService jwtService;
    private final TokenService tokenService;
    private final PasswordEncoder passwordEncoder;
    private final AuthenticationManager authenticationManager;

    public AuthResponse register(RegisterRequest request) {
        User userDetails = User
                .builder()
                .firstName(request.getFirstName())
                .username(request.getUsername())
                .email(request.getEmail())
                .password(passwordEncoder.encode(request.getPassword()))
                .role(Role.USER)
                .build();

        User savedUser = userService.saveUser(userDetails);

        String jwt = jwtService.generateToken(userDetails);
        String refreshToken = jwtService.generateRefreshToken(userDetails);

        revokeOldTokensThenSaveUserToken(savedUser, jwt);

        return AuthResponse
                .builder()
                .accessToken(jwt)
                .refreshToken(refreshToken)
                .build();
    }

    public AuthResponse authenticate(AuthRequest authRequest) {
        authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(
                        authRequest.getUsername(),
                        authRequest.getPassword()
                )
        );

        User user = userService.findUserByUsername(authRequest.getUsername());
        String jwt = jwtService.generateToken(user);
        String refreshToken = jwtService.generateRefreshToken(user);
        revokeOldTokensThenSaveUserToken(user, jwt);

        return AuthResponse
                .builder()
                .accessToken(jwt)
                .refreshToken(refreshToken)
                .build();

    }

    private Token saveToken(String jwt, User user) {
        Token token = Token
                .builder()
                .user(user)
                .token(jwt)
                .tokenType(TokenType.BEARER)
                .expired(false)
                .revoked(false)
                .build();

        Token savedToken = tokenService.saveToken(token);

        return savedToken;
    }

    private void revokeAllUserTokens(User user) {
        List<Token> allTokensByUser = tokenService.findAllByUser(user.getId());
        if (allTokensByUser.isEmpty()) return;
        allTokensByUser.forEach(token -> {
            token.setRevoked(true);
            token.setExpired(true);
        });
        tokenService.saveAll(allTokensByUser);
    }

    private void revokeOldTokensThenSaveUserToken(User user, String jwt) {
        revokeAllUserTokens(user);
        saveToken(jwt, user);
    }


}

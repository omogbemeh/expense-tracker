package codes.praise.expensetracker.token;

import codes.praise.expensetracker.user.User;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.util.List;

@Data
@RequiredArgsConstructor
@Service
public class TokenService {
    private final TokenRepository tokenRepository;

    public Token saveToken(Token token) {
        return tokenRepository.save(token);
    }

    public List<Token> saveAll(Iterable<Token> tokens) {
        return tokenRepository.saveAll(tokens);
    }

    public List<Token> findAllByUser(Long userId) {
        return tokenRepository.findAllByUser(userId);
    }
}

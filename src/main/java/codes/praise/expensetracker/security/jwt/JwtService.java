package codes.praise.expensetracker.security.jwt;

import io.jsonwebtoken.Claims;
import io.jsonwebtoken.Jwts;
import io.jsonwebtoken.SignatureAlgorithm;
import io.jsonwebtoken.io.Decoders;
import io.jsonwebtoken.security.Keys;
import lombok.*;
import org.springframework.beans.factory.annotation.Value;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.stereotype.Service;

import java.security.Key;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;
import java.util.function.Function;

@Service
@Data
@Builder
@AllArgsConstructor
@NoArgsConstructor
public class JwtService {

    @Value("${application.property.security.jwt.secret-key}")
    private String secret_key;

    @Value("${application.property.security.jwt.refreshExpiration}")
    private Long refreshExpiration;

    @Value("${application.property.security.jwt.expiration}")
    private Long expiration;

    public Claims extractAllClaims(String jwt) {
        return Jwts
                .parserBuilder()
                .setSigningKey(getSigningKey())
                .build()
                .parseClaimsJws(jwt)
                .getBody();
    }

    public String extractUsername(String jwt) {
        return extractClaim(jwt, Claims::getSubject);
    }

    private <T> T extractClaim(String jwt, Function<Claims, T> claimResolver) {
        Claims claims = extractAllClaims(jwt);
        return claimResolver.apply(claims);
    }

    public boolean isTokenValid(String jwt, UserDetails userDetails) {
        return !isTokenExpired(jwt) && extractUsername(jwt).equals(userDetails.getUsername());
    }

    private boolean isTokenExpired(String jwt) {
        return extractExpirationDate(jwt).before(new Date());
    }

    private Date extractExpirationDate(String jwt) {
        return extractClaim(jwt, Claims::getExpiration);
    }

    public String generateToken(UserDetails userDetails) {
        return buildToken(userDetails, new HashMap<>(), expiration);
    }
    public String generateToken(UserDetails userDetails, Map<String, Object> extraClaims) {
        return buildToken(userDetails, extraClaims, expiration);
    }

    private String buildToken(UserDetails userDetails, Map<String, Object> extraClaims, Long expiration) {
        return Jwts
                .builder()
                .setSubject(userDetails.getUsername())
                .setIssuedAt(new Date(System.currentTimeMillis()))
                .setExpiration(new Date(System.currentTimeMillis() + expiration))
                .signWith(getSigningKey(), SignatureAlgorithm.HS512)
                .compact();
    }

    public String generateRefreshToken(UserDetails userDetails) {
        return buildToken(userDetails, new HashMap<>(), refreshExpiration);
    }

    private Key getSigningKey() {
        byte[] bytes = Decoders.BASE64.decode(secret_key);
        return Keys.hmacShaKeyFor(bytes);
    }
}

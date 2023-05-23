package codes.praise.expensetracker.model;

import jakarta.persistence.*;
import jakarta.validation.constraints.NotNull;
import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.math.BigDecimal;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
@Entity
@Table(name = "transactions")
public class Transaction {
    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;
    @NotNull
    private BigDecimal amount;
    private String description;
    @NotNull
    @Enumerated(value = EnumType.STRING)
    private TransactionType transactionType;
    @ManyToOne(fetch = FetchType.EAGER,
            cascade = CascadeType.ALL)
    private User createdBy;
}

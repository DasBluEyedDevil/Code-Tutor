// Solution: Simple Expression Evaluator

sealed interface Expr permits Num, Add {}

record Num(int value) implements Expr {}
record Add(Expr left, Expr right) implements Expr {}

int evaluate(Expr expr) {
    return switch (expr) {
        case Num(int v) -> v;
        case Add(Expr l, Expr r) -> evaluate(l) + evaluate(r);
    };
}

void main() {
    Expr expr = new Add(new Num(3), new Num(4));
    println(evaluate(expr));
}
pub fn factorial(n:i64) {
    if (n == 1) {
        return 1;
    } else {
        return n * factorial(n - 1);
    }
}

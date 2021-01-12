use std::time::Instant;

fn main() {
    println!("Running method...");
    let timer = Instant::now();
    let fib = get_fib_sequence(35);
    let time_run = timer.elapsed();

    for i in fib {
        println!("Fib sequence: {}", i);
    }

    println!("\n--- Completed in: {:.2?} ---\n", time_run);
}

fn get_fib_sequence(n: i64) -> Vec<i64> {
    let mut sequence = Vec::new();
    for i in 0..n {
        sequence.push(fibonacci(i));
    }

    return sequence;
}

fn fibonacci(n: i64) -> i64 {
    if n == 0 {
        return 0;
    }
    if 1 == n || 2 == n {
        return 1;
    } else {
        return fibonacci(n - 1) + fibonacci(n - 2);
    }
}

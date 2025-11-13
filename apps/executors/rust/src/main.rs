use actix_cors::Cors;
use actix_web::{web, App, HttpResponse, HttpServer, Result};
use serde::{Deserialize, Serialize};
use std::fs;
use std::path::PathBuf;
use std::process::{Command, Stdio};
use std::time::Duration;
use tokio::time::timeout;
use uuid::Uuid;

#[derive(Deserialize)]
struct ExecuteRequest {
    code: String,
}

#[derive(Serialize)]
struct ExecutionResult {
    success: bool,
    output: String,
    error: Option<String>,
    #[serde(rename = "executionTime")]
    execution_time: u64,
}

#[derive(Serialize)]
struct HealthResponse {
    status: String,
    service: String,
}

async fn health() -> Result<HttpResponse> {
    Ok(HttpResponse::Ok().json(HealthResponse {
        status: "ok".to_string(),
        service: "rust-executor".to_string(),
    }))
}

async fn execute(req: web::Json<ExecuteRequest>) -> Result<HttpResponse> {
    let start = std::time::Instant::now();

    if req.code.is_empty() {
        return Ok(HttpResponse::BadRequest().json(ExecutionResult {
            success: false,
            output: String::new(),
            error: Some("No code provided".to_string()),
            execution_time: start.elapsed().as_millis() as u64,
        }));
    }

    match execute_rust_code(&req.code).await {
        Ok(result) => {
            let execution_time = start.elapsed().as_millis() as u64;
            Ok(HttpResponse::Ok().json(ExecutionResult {
                success: result.0,
                output: result.1,
                error: result.2,
                execution_time,
            }))
        }
        Err(e) => Ok(HttpResponse::InternalServerError().json(ExecutionResult {
            success: false,
            output: String::new(),
            error: Some(format!("Server error: {}", e)),
            execution_time: start.elapsed().as_millis() as u64,
        })),
    }
}

async fn execute_rust_code(code: &str) -> std::io::Result<(bool, String, Option<String>)> {
    // Create temporary directory
    let temp_dir = std::env::temp_dir().join(format!("rust-exec-{}", Uuid::new_v4()));
    fs::create_dir_all(&temp_dir)?;

    // Wrap code in main function if needed
    let code = if !code.contains("fn main") {
        format!("fn main() {{\n{}\n}}", code)
    } else {
        code.to_string()
    };

    // Write source file
    let source_file = temp_dir.join("main.rs");
    fs::write(&source_file, &code)?;

    // Compile
    let compile_output = Command::new("rustc")
        .args(&[
            source_file.to_str().unwrap(),
            "-o",
            temp_dir.join("program").to_str().unwrap(),
        ])
        .stdout(Stdio::piped())
        .stderr(Stdio::piped())
        .output()?;

    if !compile_output.status.success() {
        let error = String::from_utf8_lossy(&compile_output.stderr).to_string();
        cleanup_temp_dir(&temp_dir);
        return Ok((
            false,
            String::new(),
            Some(format!("Compilation error:\n{}", error)),
        ));
    }

    // Execute with timeout
    let executable = temp_dir.join("program");
    let execution = timeout(Duration::from_secs(5), async {
        Command::new(&executable)
            .stdout(Stdio::piped())
            .stderr(Stdio::piped())
            .output()
    })
    .await;

    cleanup_temp_dir(&temp_dir);

    match execution {
        Ok(Ok(output)) => {
            let stdout = String::from_utf8_lossy(&output.stdout).to_string();
            let stderr = String::from_utf8_lossy(&output.stderr).to_string();

            if !output.status.success() {
                Ok((
                    false,
                    stdout.clone(),
                    Some(format!("Process exited with code: {:?}\n{}", output.status.code(), stderr)),
                ))
            } else {
                let final_output = if stdout.is_empty() {
                    "(No output)".to_string()
                } else {
                    stdout
                };
                Ok((true, final_output, None))
            }
        }
        Ok(Err(e)) => Ok((
            false,
            String::new(),
            Some(format!("Execution error: {}", e)),
        )),
        Err(_) => Ok((
            false,
            String::new(),
            Some("Execution timed out after 5 seconds".to_string()),
        )),
    }
}

fn cleanup_temp_dir(dir: &PathBuf) {
    let _ = fs::remove_dir_all(dir);
}

#[actix_web::main]
async fn main() -> std::io::Result<()> {
    println!("🦀 Rust executor service running on port 4003...");

    HttpServer::new(|| {
        let cors = Cors::permissive();

        App::new()
            .wrap(cors)
            .route("/health", web::get().to(health))
            .route("/execute", web::post().to(execute))
    })
    .bind(("0.0.0.0", 4003))?
    .run()
    .await
}

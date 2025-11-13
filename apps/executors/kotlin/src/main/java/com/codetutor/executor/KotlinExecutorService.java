package com.codetutor.executor;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import spark.Request;
import spark.Response;

import java.io.*;
import java.nio.file.*;
import java.util.*;
import java.util.concurrent.*;

import static spark.Spark.*;

public class KotlinExecutorService {

    private static final Gson gson = new Gson();
    private static final long EXECUTION_TIMEOUT = 5000; // 5 seconds
    private static final int MAX_OUTPUT_LENGTH = 10000;

    public static void main(String[] args) {
        port(4002);

        before((request, response) -> {
            response.header("Access-Control-Allow-Origin", "*");
            response.header("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.header("Access-Control-Allow-Headers", "Content-Type, Authorization");
        });

        options("/*", (request, response) -> "OK");

        get("/health", KotlinExecutorService::healthCheck);
        post("/execute", KotlinExecutorService::execute);

        System.out.println("🟣 Kotlin executor service running on port 4002...");
    }

    private static String healthCheck(Request req, Response res) {
        res.type("application/json");
        JsonObject response = new JsonObject();
        response.addProperty("status", "ok");
        response.addProperty("service", "kotlin-executor");
        return gson.toJson(response);
    }

    private static String execute(Request req, Response res) {
        res.type("application/json");

        try {
            JsonObject body = gson.fromJson(req.body(), JsonObject.class);
            String code = body.get("code").getAsString();

            if (code == null || code.isEmpty()) {
                JsonObject error = new JsonObject();
                error.addProperty("success", false);
                error.addProperty("error", "No code provided");
                res.status(400);
                return gson.toJson(error);
            }

            ExecutionResult result = executeKotlinCode(code);
            return gson.toJson(result);

        } catch (Exception e) {
            JsonObject error = new JsonObject();
            error.addProperty("success", false);
            error.addProperty("error", "Server error: " + e.getMessage());
            res.status(500);
            return gson.toJson(error);
        }
    }

    private static ExecutionResult executeKotlinCode(String code) {
        long startTime = System.currentTimeMillis();

        try {
            // Create temporary directory
            Path tempDir = Files.createTempDirectory("kotlin-exec-");
            String fileName = "Main.kt";

            // Wrap code in main function if needed
            if (!code.contains("fun main")) {
                code = "fun main() {\n" + code + "\n}";
            }

            // Write source file
            Path sourceFile = tempDir.resolve(fileName);
            Files.writeString(sourceFile, code);

            // Compile using kotlinc
            ProcessBuilder compileBuilder = new ProcessBuilder(
                "kotlinc",
                sourceFile.toString(),
                "-include-runtime",
                "-d", tempDir.resolve("output.jar").toString()
            );
            compileBuilder.directory(tempDir.toFile());
            compileBuilder.redirectErrorStream(true);

            Process compileProcess = compileBuilder.start();
            String compileOutput = readProcessOutput(compileProcess);

            if (!compileProcess.waitFor(10, TimeUnit.SECONDS)) {
                compileProcess.destroyForcibly();
                cleanup(tempDir);
                return new ExecutionResult(false, "", "Compilation timed out",
                    System.currentTimeMillis() - startTime);
            }

            int compileExitCode = compileProcess.exitValue();
            if (compileExitCode != 0) {
                cleanup(tempDir);
                return new ExecutionResult(false, "", "Compilation error:\n" + compileOutput,
                    System.currentTimeMillis() - startTime);
            }

            // Execute the compiled jar
            ProcessBuilder runBuilder = new ProcessBuilder(
                "java",
                "-jar",
                tempDir.resolve("output.jar").toString()
            );
            runBuilder.directory(tempDir.toFile());
            runBuilder.redirectErrorStream(true);

            Process runProcess = runBuilder.start();

            // Capture output with timeout
            ExecutorService executor = Executors.newSingleThreadExecutor();
            Future<String> outputFuture = executor.submit(() -> readProcessOutput(runProcess));

            String output;
            try {
                if (!runProcess.waitFor(EXECUTION_TIMEOUT, TimeUnit.MILLISECONDS)) {
                    runProcess.destroy();
                    runProcess.waitFor(1, TimeUnit.SECONDS);
                    if (runProcess.isAlive()) {
                        runProcess.destroyForcibly();
                    }
                    executor.shutdownNow();
                    cleanup(tempDir);
                    return new ExecutionResult(false, "", "Execution timed out after 5 seconds",
                        System.currentTimeMillis() - startTime);
                }

                output = outputFuture.get(1, TimeUnit.SECONDS);
            } catch (TimeoutException e) {
                runProcess.destroyForcibly();
                executor.shutdownNow();
                cleanup(tempDir);
                return new ExecutionResult(false, "", "Execution timed out",
                    System.currentTimeMillis() - startTime);
            }

            executor.shutdown();

            int exitCode = runProcess.exitValue();
            cleanup(tempDir);

            if (exitCode != 0) {
                return new ExecutionResult(false, output, "Process exited with code: " + exitCode,
                    System.currentTimeMillis() - startTime);
            }

            return new ExecutionResult(true, output, null,
                System.currentTimeMillis() - startTime);

        } catch (Exception e) {
            return new ExecutionResult(false, "", "Execution error: " + e.getMessage(),
                System.currentTimeMillis() - startTime);
        }
    }

    private static String readProcessOutput(Process process) throws IOException {
        BufferedReader reader = new BufferedReader(
            new InputStreamReader(process.getInputStream())
        );
        StringBuilder output = new StringBuilder();
        String line;
        while ((line = reader.readLine()) != null) {
            output.append(line).append("\n");
            if (output.length() > MAX_OUTPUT_LENGTH) {
                break;
            }
        }
        return output.toString();
    }

    private static void cleanup(Path directory) {
        try {
            Files.walk(directory)
                .sorted(Comparator.reverseOrder())
                .forEach(path -> {
                    try {
                        Files.delete(path);
                    } catch (IOException e) {
                        // Ignore
                    }
                });
        } catch (IOException e) {
            // Ignore cleanup errors
        }
    }

    static class ExecutionResult {
        boolean success;
        String output;
        String error;
        long executionTime;

        ExecutionResult(boolean success, String output, String error, long executionTime) {
            this.success = success;
            this.output = output == null || output.isEmpty() ? "(No output)" : output;
            this.error = error;
            this.executionTime = executionTime;
        }
    }
}

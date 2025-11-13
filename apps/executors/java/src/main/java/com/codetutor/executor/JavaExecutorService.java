package com.codetutor.executor;

import com.google.gson.Gson;
import com.google.gson.JsonObject;
import spark.Request;
import spark.Response;

import javax.tools.*;
import java.io.*;
import java.net.URI;
import java.nio.file.*;
import java.util.*;
import java.util.concurrent.*;

import static spark.Spark.*;

public class JavaExecutorService {

    private static final Gson gson = new Gson();
    private static final long EXECUTION_TIMEOUT = 5000; // 5 seconds
    private static final int MAX_OUTPUT_LENGTH = 10000;

    public static void main(String[] args) {
        port(4001);

        // Enable CORS
        before((request, response) -> {
            response.header("Access-Control-Allow-Origin", "*");
            response.header("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.header("Access-Control-Allow-Headers", "Content-Type, Authorization");
        });

        options("/*", (request, response) -> "OK");

        get("/health", JavaExecutorService::healthCheck);
        post("/execute", JavaExecutorService::execute);

        System.out.println("☕ Java executor service running on port 4001...");
    }

    private static String healthCheck(Request req, Response res) {
        res.type("application/json");
        JsonObject response = new JsonObject();
        response.addProperty("status", "ok");
        response.addProperty("service", "java-executor");
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

            ExecutionResult result = executeJavaCode(code);
            return gson.toJson(result);

        } catch (Exception e) {
            JsonObject error = new JsonObject();
            error.addProperty("success", false);
            error.addProperty("error", "Server error: " + e.getMessage());
            res.status(500);
            return gson.toJson(error);
        }
    }

    private static ExecutionResult executeJavaCode(String code) {
        long startTime = System.currentTimeMillis();

        try {
            // Create temporary directory for this execution
            Path tempDir = Files.createTempDirectory("java-exec-");

            // Extract class name from code
            String className = extractClassName(code);
            if (className == null) {
                className = "Main";
                // Wrap code in a Main class if needed
                if (!code.contains("class " + className)) {
                    code = "public class " + className + " {\n" + code + "\n}";
                }
            }

            // Write source file
            Path sourceFile = tempDir.resolve(className + ".java");
            Files.writeString(sourceFile, code);

            // Compile
            JavaCompiler compiler = ToolProvider.getSystemJavaCompiler();
            if (compiler == null) {
                return new ExecutionResult(false, "", "Java compiler not available",
                    System.currentTimeMillis() - startTime);
            }

            ByteArrayOutputStream errorStream = new ByteArrayOutputStream();
            StandardJavaFileManager fileManager = compiler.getStandardFileManager(null, null, null);

            Iterable<? extends JavaFileObject> compilationUnits =
                fileManager.getJavaFileObjects(sourceFile.toFile());

            JavaCompiler.CompilationTask task = compiler.getTask(
                new OutputStreamWriter(errorStream),
                fileManager,
                null,
                Arrays.asList("-d", tempDir.toString()),
                null,
                compilationUnits
            );

            boolean success = task.call();
            fileManager.close();

            if (!success) {
                String compileError = errorStream.toString();
                cleanup(tempDir);
                return new ExecutionResult(false, "", "Compilation error:\n" + compileError,
                    System.currentTimeMillis() - startTime);
            }

            // Execute
            ProcessBuilder pb = new ProcessBuilder(
                "java",
                "-cp", tempDir.toString(),
                "-Djava.security.manager=allow",
                className
            );
            pb.directory(tempDir.toFile());
            pb.redirectErrorStream(true);

            Process process = pb.start();

            // Capture output with timeout
            ExecutorService executor = Executors.newSingleThreadExecutor();
            Future<String> outputFuture = executor.submit(() -> {
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
            });

            String output;
            try {
                if (!process.waitFor(EXECUTION_TIMEOUT, TimeUnit.MILLISECONDS)) {
                    process.destroy();
                    process.waitFor(1, TimeUnit.SECONDS);
                    if (process.isAlive()) {
                        process.destroyForcibly();
                    }
                    executor.shutdownNow();
                    cleanup(tempDir);
                    return new ExecutionResult(false, "", "Execution timed out after 5 seconds",
                        System.currentTimeMillis() - startTime);
                }

                output = outputFuture.get(1, TimeUnit.SECONDS);
            } catch (TimeoutException e) {
                process.destroyForcibly();
                executor.shutdownNow();
                cleanup(tempDir);
                return new ExecutionResult(false, "", "Execution timed out",
                    System.currentTimeMillis() - startTime);
            }

            executor.shutdown();

            int exitCode = process.exitValue();
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

    private static String extractClassName(String code) {
        // Simple regex to extract class name
        String[] lines = code.split("\n");
        for (String line : lines) {
            line = line.trim();
            if (line.startsWith("public class ") || line.startsWith("class ")) {
                String[] parts = line.split("\\s+");
                for (int i = 0; i < parts.length - 1; i++) {
                    if (parts[i].equals("class")) {
                        String className = parts[i + 1];
                        // Remove any braces or implements/extends
                        className = className.split("[\\{\\s]")[0];
                        return className;
                    }
                }
            }
        }
        return null;
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

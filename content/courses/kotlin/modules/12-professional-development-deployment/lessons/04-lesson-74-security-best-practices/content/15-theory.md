---
type: "THEORY"
title: "Solution 3"
---



---



```kotlin
class FileUploadService(
    private val uploadDir: File,
    private val maxFileSize: Long = 10 * 1024 * 1024, // 10 MB
    private val allowedExtensions: Set<String> = setOf("jpg", "png", "pdf")
) {
    init {
        if (!uploadDir.exists()) {
            uploadDir.mkdirs()
        }
    }

    suspend fun upload(
        file: MultiPartData,
        userId: String
    ): Result<UploadedFile> {
        var uploadedFile: UploadedFile? = null
        var tempFile: File? = null

        try {
            file.forEachPart { part ->
                when (part) {
                    is PartData.FileItem -> {
                        val fileName = part.originalFileName ?: return@forEachPart

                        // Validate filename
                        if (!isValidFilename(fileName)) {
                            return Result.failure(ValidationException("Invalid filename"))
                        }

                        // Validate extension
                        val extension = fileName.substringAfterLast(".", "")
                        if (extension.lowercase() !in allowedExtensions) {
                            return Result.failure(
                                ValidationException("File type not allowed. Allowed: $allowedExtensions")
                            )
                        }

                        // Generate safe filename
                        val safeFilename = "${UUID.randomUUID()}.${extension.lowercase()}"
                        tempFile = File(uploadDir, safeFilename)

                        var size = 0L
                        tempFile!!.outputStream().use { output ->
                            part.streamProvider().use { input ->
                                val buffer = ByteArray(8192)
                                var bytesRead: Int

                                while (input.read(buffer).also { bytesRead = it } != -1) {
                                    size += bytesRead

                                    if (size > maxFileSize) {
                                        return Result.failure(
                                            ValidationException("File too large. Max: ${maxFileSize / 1024 / 1024}MB")
                                        )
                                    }

                                    output.write(buffer, 0, bytesRead)
                                }
                            }
                        }

                        // Validate file type (magic numbers)
                        if (!isValidFileType(tempFile!!, extension)) {
                            tempFile!!.delete()
                            return Result.failure(ValidationException("File content doesn't match extension"))
                        }

                        // Scan for malware (integrate with antivirus)
                        if (containsMalware(tempFile!!)) {
                            tempFile!!.delete()
                            return Result.failure(SecurityException("Malware detected"))
                        }

                        uploadedFile = UploadedFile(
                            id = UUID.randomUUID().toString(),
                            originalFilename = fileName,
                            storedFilename = safeFilename,
                            extension = extension,
                            size = size,
                            uploadedBy = userId,
                            uploadedAt = System.currentTimeMillis()
                        )
                    }
                    else -> {}
                }
                part.dispose()
            }

            return uploadedFile?.let { Result.success(it) }
                ?: Result.failure(Exception("No file uploaded"))

        } catch (e: Exception) {
            tempFile?.delete()
            return Result.failure(e)
        }
    }

    private fun isValidFilename(filename: String): Boolean {
        // No path traversal
        if (filename.contains("..") || filename.contains("/") || filename.contains("\\")) {
            return false
        }

        // No special characters
        if (!filename.matches(Regex("^[a-zA-Z0-9._-]+$"))) {
            return false
        }

        return true
    }

    private fun isValidFileType(file: File, expectedExtension: String): Boolean {
        val bytes = file.inputStream().use { it.readNBytes(12) }

        return when (expectedExtension.lowercase()) {
            "jpg", "jpeg" -> bytes.take(3).toByteArray().contentEquals(
                byteArrayOf(0xFF.toByte(), 0xD8.toByte(), 0xFF.toByte())
            )
            "png" -> bytes.take(8).toByteArray().contentEquals(
                byteArrayOf(0x89.toByte(), 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A)
            )
            "pdf" -> bytes.take(4).toByteArray().contentEquals(
                byteArrayOf(0x25, 0x50, 0x44, 0x46) // %PDF
            )
            else -> false
        }
    }

    private fun containsMalware(file: File): Boolean {
        // Integrate with ClamAV or similar
        // For now, return false
        return false
    }
}

data class UploadedFile(
    val id: String,
    val originalFilename: String,
    val storedFilename: String,
    val extension: String,
    val size: Long,
    val uploadedBy: String,
    val uploadedAt: Long
)

// Ktor route
fun Route.fileUpload(fileUploadService: FileUploadService) {
    post("/upload") {
        val principal = call.principal<UserPrincipal>()
            ?: return@post call.respond(HttpStatusCode.Unauthorized)

        val multipart = call.receiveMultipart()

        val result = fileUploadService.upload(multipart, principal.id)

        result.fold(
            onSuccess = { uploadedFile ->
                call.respond(HttpStatusCode.Created, uploadedFile)
            },
            onFailure = { error ->
                when (error) {
                    is ValidationException -> call.respond(
                        HttpStatusCode.BadRequest,
                        mapOf("error" to error.message)
                    )
                    is SecurityException -> call.respond(
                        HttpStatusCode.Forbidden,
                        mapOf("error" to error.message)
                    )
                    else -> call.respond(
                        HttpStatusCode.InternalServerError,
                        mapOf("error" to "Upload failed")
                    )
                }
            }
        )
    }
}
```

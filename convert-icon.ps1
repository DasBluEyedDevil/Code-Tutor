Add-Type -AssemblyName System.Drawing

$sourceImage = [System.Drawing.Image]::FromFile('C:\Users\dasbl\Downloads\code-tutor-icon.png')
$outputPath = 'C:\Users\dasbl\Downloads\Code-Tutor\native-app\Assets\icon.ico'

# Create multi-size ICO
$sizes = @(16, 32, 48, 256)
$ms = New-Object System.IO.MemoryStream
$bw = New-Object System.IO.BinaryWriter($ms)

# ICO header
$bw.Write([Int16]0)  # Reserved
$bw.Write([Int16]1)  # Type (1 = ICO)
$bw.Write([Int16]$sizes.Count)  # Number of images

$imageDataList = @()
$offset = 6 + ($sizes.Count * 16)  # Header + directory entries

foreach ($size in $sizes) {
    $bmp = New-Object System.Drawing.Bitmap($sourceImage, $size, $size)
    $imgMs = New-Object System.IO.MemoryStream
    $bmp.Save($imgMs, [System.Drawing.Imaging.ImageFormat]::Png)
    $imgData = $imgMs.ToArray()
    $imageDataList += ,$imgData

    # Directory entry
    $widthByte = if ($size -eq 256) { 0 } else { $size }
    $heightByte = if ($size -eq 256) { 0 } else { $size }
    $bw.Write([Byte]$widthByte)  # Width (0 means 256)
    $bw.Write([Byte]$heightByte)  # Height
    $bw.Write([Byte]0)  # Color palette
    $bw.Write([Byte]0)  # Reserved
    $bw.Write([Int16]1)  # Color planes
    $bw.Write([Int16]32) # Bits per pixel
    $bw.Write([Int32]$imgData.Length)  # Size of image data
    $bw.Write([Int32]$offset)  # Offset to image data

    $offset += $imgData.Length
    $bmp.Dispose()
    $imgMs.Dispose()
}

# Write image data
foreach ($imgData in $imageDataList) {
    $bw.Write($imgData)
}

# Save to file
[System.IO.File]::WriteAllBytes($outputPath, $ms.ToArray())

$bw.Dispose()
$ms.Dispose()
$sourceImage.Dispose()

Write-Host "Icon created successfully!"
Get-Item $outputPath | Select-Object Name, Length

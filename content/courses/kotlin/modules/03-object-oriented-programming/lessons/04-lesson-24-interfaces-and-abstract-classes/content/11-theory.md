---
type: "THEORY"
title: "Solution: Media Player System"
---



---



```kotlin
interface Playable {
    val title: String
    var isPlaying: Boolean

    fun play() {
        isPlaying = true
        println("▶️  Playing: $title")
    }

    fun pause() {
        isPlaying = false
        println("⏸️  Paused: $title")
    }

    fun stop() {
        isPlaying = false
        println("⏹️  Stopped: $title")
    }
}

interface Downloadable {
    val sizeInMB: Double

    fun download() {
        println("⬇️  Downloading... ($sizeInMB MB)")
        println("✅ Download complete!")
    }
}

class Song(
    override val title: String,
    val artist: String,
    override val sizeInMB: Double
) : Playable, Downloadable {
    override var isPlaying: Boolean = false

    override fun play() {
        println("🎵 Song")
        super.play()
        println("   Artist: $artist")
    }
}

class Podcast(
    override val title: String,
    val host: String,
    val episode: Int,
    override val sizeInMB: Double
) : Playable, Downloadable {
    override var isPlaying: Boolean = false

    override fun play() {
        println("🎙️  Podcast")
        super.play()
        println("   Host: $host, Episode: $episode")
    }
}

class LiveStream(
    override val title: String,
    val streamer: String
) : Playable {
    override var isPlaying: Boolean = false

    override fun play() {
        println("📡 Live Stream")
        super.play()
        println("   Streamer: $streamer")
    }
}

class MediaPlayer {
    private val playlist = mutableListOf<Playable>()
    private var currentIndex = 0

    fun addToPlaylist(item: Playable) {
        playlist.add(item)
        println("Added to playlist: ${item.title}")
    }

    fun playAll() {
        println("\n=== Playing All ===")
        playlist.forEach { it.play() }
    }

    fun downloadAll() {
        println("\n=== Downloading All (if possible) ===")
        playlist.forEach { item ->
            if (item is Downloadable) {
                item.download()
            } else {
                println("⚠️  ${item.title} cannot be downloaded (live stream)")
            }
        }
    }
}

fun main() {
    val player = MediaPlayer()

    val song1 = Song("Bohemian Rhapsody", "Queen", 5.8)
    val song2 = Song("Imagine", "John Lennon", 3.2)
    val podcast = Podcast("Tech Talk Daily", "Jane Doe", 42, 25.5)
    val stream = LiveStream("Gaming Night", "ProGamer123")

    player.addToPlaylist(song1)
    player.addToPlaylist(song2)
    player.addToPlaylist(podcast)
    player.addToPlaylist(stream)

    player.playAll()
    player.downloadAll()
}
```

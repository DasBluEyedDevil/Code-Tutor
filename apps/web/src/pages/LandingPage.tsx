import { Link } from 'react-router-dom'
import { Code2, Moon, Sun, BookOpen, Zap, Award } from 'lucide-react'
import { useThemeStore } from '../stores/themeStore'

const languages = [
  { id: 'java', name: 'Java', color: 'bg-java', description: 'Full-stack Java development', lessons: 20 },
  { id: 'python', name: 'Python', color: 'bg-python', description: 'From basics to web development', lessons: 73 },
  { id: 'kotlin', name: 'Kotlin', color: 'bg-kotlin', description: 'Modern JVM language', lessons: 29 },
  { id: 'rust', name: 'Rust', color: 'bg-rust', description: 'Systems programming', lessons: 60 },
  { id: 'csharp', name: 'C#', color: 'bg-csharp', description: '.NET development', lessons: 26 },
  { id: 'flutter', name: 'Flutter', color: 'bg-flutter', description: 'Cross-platform mobile apps', lessons: 95 },
  { id: 'javascript-typescript', name: 'JS/TS', color: 'bg-javascript', description: 'Web development', lessons: 0, comingSoon: true },
]

export default function LandingPage() {
  const { theme, toggleTheme } = useThemeStore()

  return (
    <div className="min-h-screen bg-gradient-to-b from-background to-secondary/20">
      {/* Header */}
      <header className="border-b">
        <div className="container mx-auto px-4 py-4 flex justify-between items-center">
          <div className="flex items-center gap-2">
            <Code2 className="w-8 h-8 text-primary" />
            <h1 className="text-2xl font-bold">Code Tutor</h1>
          </div>
          <button
            onClick={toggleTheme}
            className="p-2 rounded-lg hover:bg-secondary transition-colors"
          >
            {theme === 'dark' ? <Sun className="w-5 h-5" /> : <Moon className="w-5 h-5" />}
          </button>
        </div>
      </header>

      {/* Hero Section */}
      <section className="container mx-auto px-4 py-16 text-center">
        <h2 className="text-5xl font-bold mb-6">
          Master Multiple Programming Languages
        </h2>
        <p className="text-xl text-muted-foreground mb-8 max-w-2xl mx-auto">
          One unified platform. Seven languages. Interactive lessons with real-time code execution.
          From absolute beginner to full-stack developer.
        </p>

        {/* Features */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8 mt-12 max-w-4xl mx-auto">
          <div className="p-6 rounded-lg bg-card border">
            <BookOpen className="w-12 h-12 mx-auto mb-4 text-primary" />
            <h3 className="text-lg font-semibold mb-2">Concept-First Learning</h3>
            <p className="text-sm text-muted-foreground">
              Understand concepts before jargon with our proven teaching approach
            </p>
          </div>
          <div className="p-6 rounded-lg bg-card border">
            <Zap className="w-12 h-12 mx-auto mb-4 text-primary" />
            <h3 className="text-lg font-semibold mb-2">Instant Execution</h3>
            <p className="text-sm text-muted-foreground">
              Write and run code directly in your browser. No setup required.
            </p>
          </div>
          <div className="p-6 rounded-lg bg-card border">
            <Award className="w-12 h-12 mx-auto mb-4 text-primary" />
            <h3 className="text-lg font-semibold mb-2">Track Progress</h3>
            <p className="text-sm text-muted-foreground">
              See your advancement across all languages with detailed analytics
            </p>
          </div>
        </div>
      </section>

      {/* Language Selection */}
      <section className="container mx-auto px-4 py-16">
        <h3 className="text-3xl font-bold text-center mb-12">Choose Your Language</h3>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6 max-w-7xl mx-auto">
          {languages.map((lang) => (
            <Link
              key={lang.id}
              to={lang.comingSoon ? '#' : `/course/${lang.id}`}
              className={`group relative p-6 rounded-xl border-2 bg-card transition-all hover:scale-105 hover:shadow-lg ${
                lang.comingSoon ? 'opacity-60 cursor-not-allowed' : 'hover:border-primary'
              }`}
              onClick={(e) => lang.comingSoon && e.preventDefault()}
            >
              {lang.comingSoon && (
                <div className="absolute top-2 right-2 bg-yellow-500 text-black text-xs px-2 py-1 rounded-full font-semibold">
                  Coming Soon
                </div>
              )}
              <div className={`w-16 h-16 ${lang.color} rounded-lg mb-4 flex items-center justify-center`}>
                <Code2 className="w-8 h-8 text-white" />
              </div>
              <h4 className="text-2xl font-bold mb-2">{lang.name}</h4>
              <p className="text-sm text-muted-foreground mb-4">{lang.description}</p>
              <div className="flex items-center justify-between">
                <span className="text-sm font-medium">{lang.lessons} Lessons</span>
                {!lang.comingSoon && (
                  <span className="text-sm text-primary group-hover:translate-x-1 transition-transform">
                    Start Learning →
                  </span>
                )}
              </div>
            </Link>
          ))}
        </div>
      </section>

      {/* Footer */}
      <footer className="border-t mt-16">
        <div className="container mx-auto px-4 py-8 text-center text-sm text-muted-foreground">
          <p>© 2025 Code Tutor. Built with ❤️ for developers learning multiple languages.</p>
        </div>
      </footer>
    </div>
  )
}

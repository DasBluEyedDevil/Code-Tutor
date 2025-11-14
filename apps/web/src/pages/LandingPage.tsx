import { Link } from 'react-router-dom'
import { Code2, Moon, Sun, BookOpen, Zap, Award, CheckCircle2, Users, Globe } from 'lucide-react'
import { useThemeStore } from '../stores/themeStore'
import { Card, CardContent } from '../components/Card'
import { Badge } from '../components/Badge'
import { Button } from '../components/Button'

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
    <div className="min-h-screen bg-gradient-to-b from-background via-secondary/10 to-background">
      {/* Header */}
      <header className="border-b backdrop-blur-sm bg-background/80 sticky top-0 z-50">
        <div className="container mx-auto px-4 py-4 flex justify-between items-center">
          <div className="flex items-center gap-2">
            <div className="bg-gradient-to-br from-primary to-primary/60 p-2 rounded-lg">
              <Code2 className="w-6 h-6 text-white" />
            </div>
            <h1 className="text-2xl font-bold bg-gradient-to-r from-primary to-primary/60 bg-clip-text text-transparent">
              Code Tutor
            </h1>
          </div>
          <Button
            variant="ghost"
            size="sm"
            onClick={toggleTheme}
            className="rounded-full"
          >
            {theme === 'dark' ? <Sun className="w-5 h-5" /> : <Moon className="w-5 h-5" />}
          </Button>
        </div>
      </header>

      {/* Hero Section */}
      <section className="container mx-auto px-4 py-20 md:py-28 text-center animate-fade-in">
        <Badge variant="info" className="mb-6 text-sm px-4 py-1.5">
          7 Programming Languages • Real-Time Execution
        </Badge>
        <h2 className="text-4xl md:text-6xl font-bold mb-6 leading-tight">
          Master Multiple
          <br />
          <span className="bg-gradient-to-r from-primary via-blue-500 to-purple-500 bg-clip-text text-transparent">
            Programming Languages
          </span>
        </h2>
        <p className="text-lg md:text-xl text-muted-foreground mb-10 max-w-2xl mx-auto leading-relaxed">
          One unified platform. Seven languages. Interactive lessons with real-time code execution.
          Transform from absolute beginner to full-stack developer.
        </p>

        {/* Stats */}
        <div className="flex flex-wrap justify-center gap-6 md:gap-12 mb-12">
          <div className="text-center">
            <div className="text-3xl md:text-4xl font-bold text-primary">340+</div>
            <div className="text-sm text-muted-foreground">Lessons</div>
          </div>
          <div className="text-center">
            <div className="text-3xl md:text-4xl font-bold text-primary">7</div>
            <div className="text-sm text-muted-foreground">Languages</div>
          </div>
          <div className="text-center">
            <div className="text-3xl md:text-4xl font-bold text-primary">100%</div>
            <div className="text-sm text-muted-foreground">Free</div>
          </div>
        </div>

        {/* Features */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-16 max-w-4xl mx-auto">
          <Card hover className="animate-fade-in-up" style={{ animationDelay: '100ms' }}>
            <CardContent className="pt-6 text-center">
              <div className="bg-gradient-to-br from-blue-500/10 to-blue-500/5 p-4 rounded-xl inline-block mb-4">
                <BookOpen className="w-10 h-10 text-blue-500" />
              </div>
              <h3 className="text-lg font-semibold mb-2">Concept-First Learning</h3>
              <p className="text-sm text-muted-foreground leading-relaxed">
                Understand concepts before jargon with our proven teaching approach
              </p>
            </CardContent>
          </Card>
          <Card hover className="animate-fade-in-up" style={{ animationDelay: '200ms' }}>
            <CardContent className="pt-6 text-center">
              <div className="bg-gradient-to-br from-green-500/10 to-green-500/5 p-4 rounded-xl inline-block mb-4">
                <Zap className="w-10 h-10 text-green-500" />
              </div>
              <h3 className="text-lg font-semibold mb-2">Instant Execution</h3>
              <p className="text-sm text-muted-foreground leading-relaxed">
                Write and run code directly in your browser. No setup required.
              </p>
            </CardContent>
          </Card>
          <Card hover className="animate-fade-in-up" style={{ animationDelay: '300ms' }}>
            <CardContent className="pt-6 text-center">
              <div className="bg-gradient-to-br from-purple-500/10 to-purple-500/5 p-4 rounded-xl inline-block mb-4">
                <Award className="w-10 h-10 text-purple-500" />
              </div>
              <h3 className="text-lg font-semibold mb-2">Track Progress</h3>
              <p className="text-sm text-muted-foreground leading-relaxed">
                See your advancement across all languages with detailed analytics
              </p>
            </CardContent>
          </Card>
        </div>
      </section>

      {/* Language Selection */}
      <section className="container mx-auto px-4 py-16 md:py-24">
        <div className="text-center mb-12">
          <h3 className="text-3xl md:text-4xl font-bold mb-4">Choose Your Language</h3>
          <p className="text-muted-foreground max-w-2xl mx-auto">
            Start learning any of our 7 programming languages. All courses include interactive lessons,
            real-time code execution, and progress tracking.
          </p>
        </div>
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6 max-w-7xl mx-auto">
          {languages.map((lang, index) => (
            <Link
              key={lang.id}
              to={lang.comingSoon ? '#' : `/course/${lang.id}`}
              className={`group relative block animate-fade-in-up`}
              style={{ animationDelay: `${index * 50}ms` }}
              onClick={(e) => lang.comingSoon && e.preventDefault()}
            >
              <Card
                hover={!lang.comingSoon}
                className={`h-full transition-all duration-300 ${
                  lang.comingSoon ? 'opacity-60 cursor-not-allowed' : 'hover:border-primary/50'
                }`}
              >
                <CardContent className="p-6">
                  {lang.comingSoon && (
                    <Badge variant="warning" className="absolute top-4 right-4">
                      Coming Soon
                    </Badge>
                  )}
                  <div className="relative mb-4">
                    <div
                      className={`w-16 h-16 ${lang.color} rounded-xl flex items-center justify-center shadow-lg transform group-hover:scale-110 transition-transform duration-300`}
                    >
                      <Code2 className="w-8 h-8 text-white" />
                    </div>
                  </div>
                  <h4 className="text-2xl font-bold mb-2 group-hover:text-primary transition-colors">
                    {lang.name}
                  </h4>
                  <p className="text-sm text-muted-foreground mb-4 line-clamp-2">
                    {lang.description}
                  </p>
                  <div className="flex items-center justify-between pt-4 border-t">
                    <div className="flex items-center gap-2">
                      <BookOpen className="w-4 h-4 text-muted-foreground" />
                      <span className="text-sm font-medium">{lang.lessons} Lessons</span>
                    </div>
                    {!lang.comingSoon && (
                      <CheckCircle2 className="w-5 h-5 text-green-500" />
                    )}
                  </div>
                  {!lang.comingSoon && (
                    <div className="mt-4">
                      <Button variant="outline" size="sm" className="w-full group-hover:bg-primary group-hover:text-primary-foreground">
                        Start Learning →
                      </Button>
                    </div>
                  )}
                </CardContent>
              </Card>
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

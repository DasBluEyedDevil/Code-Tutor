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
    <div className="min-h-screen bg-gradient-to-b from-background via-secondary/10 to-background relative overflow-hidden">
      {/* Animated Background Elements */}
      <div className="absolute inset-0 overflow-hidden pointer-events-none">
        <div className="absolute top-20 left-10 w-72 h-72 bg-blue-500/10 rounded-full blur-3xl animate-float"></div>
        <div className="absolute top-40 right-10 w-96 h-96 bg-purple-500/10 rounded-full blur-3xl animate-float" style={{ animationDelay: '2s' }}></div>
        <div className="absolute bottom-20 left-1/3 w-80 h-80 bg-green-500/10 rounded-full blur-3xl animate-float" style={{ animationDelay: '4s' }}></div>
      </div>

      {/* Header */}
      <header className="border-b glass sticky top-0 z-50 shine" role="banner">
        <nav className="container mx-auto px-4 py-4 flex justify-between items-center" aria-label="Main navigation">
          <div className="flex items-center gap-2 animate-scale-in">
            <div className="bg-gradient-to-br from-primary via-blue-500 to-purple-500 p-2 rounded-lg shadow-lg animate-gradient" aria-hidden="true">
              <Code2 className="w-6 h-6 text-white" />
            </div>
            <h1 className="text-2xl font-bold bg-gradient-to-r from-primary via-blue-500 to-purple-500 bg-clip-text text-transparent animate-gradient">
              Code Tutor
            </h1>
          </div>
          <Button
            variant="ghost"
            size="sm"
            onClick={toggleTheme}
            aria-label={theme === 'dark' ? 'Switch to light mode' : 'Switch to dark mode'}
            className="rounded-full hover:scale-110 transition-transform"
          >
            {theme === 'dark' ? <Sun className="w-5 h-5" aria-hidden="true" /> : <Moon className="w-5 h-5" aria-hidden="true" />}
          </Button>
        </nav>
      </header>

      {/* Hero Section */}
      <section id="main-content" tabIndex={-1} className="container mx-auto px-4 py-20 md:py-28 text-center animate-fade-in relative z-10">
        <Badge variant="info" className="mb-6 text-sm px-4 py-1.5 shadow-lg animate-bounce-subtle">
          7 Programming Languages • Real-Time Execution
        </Badge>
        <h2 className="text-4xl md:text-6xl lg:text-7xl font-bold mb-6 leading-tight">
          Master Multiple
          <br />
          <span className="bg-gradient-to-r from-primary via-blue-500 to-purple-500 bg-clip-text text-transparent animate-gradient neon-text">
            Programming Languages
          </span>
        </h2>
        <p className="text-lg md:text-xl text-muted-foreground mb-10 max-w-2xl mx-auto leading-relaxed animate-fade-in-up" style={{ animationDelay: '200ms' }}>
          One unified platform. Seven languages. Interactive lessons with real-time code execution.
          Transform from absolute beginner to full-stack developer.
        </p>

        {/* Stats */}
        <div className="flex flex-wrap justify-center gap-6 md:gap-12 mb-12 animate-fade-in-up" style={{ animationDelay: '400ms' }}>
          <div className="text-center group cursor-default">
            <div className="text-3xl md:text-4xl font-bold bg-gradient-to-r from-blue-500 to-cyan-500 bg-clip-text text-transparent group-hover:scale-110 transition-transform">340+</div>
            <div className="text-sm text-muted-foreground">Lessons</div>
          </div>
          <div className="text-center group cursor-default">
            <div className="text-3xl md:text-4xl font-bold bg-gradient-to-r from-purple-500 to-pink-500 bg-clip-text text-transparent group-hover:scale-110 transition-transform">7</div>
            <div className="text-sm text-muted-foreground">Languages</div>
          </div>
          <div className="text-center group cursor-default">
            <div className="text-3xl md:text-4xl font-bold bg-gradient-to-r from-green-500 to-emerald-500 bg-clip-text text-transparent group-hover:scale-110 transition-transform">100%</div>
            <div className="text-sm text-muted-foreground">Free</div>
          </div>
        </div>

        {/* Features */}
        <div className="grid grid-cols-1 md:grid-cols-3 gap-6 mt-16 max-w-4xl mx-auto">
          <Card hover className="animate-fade-in-up shine group border-blue-500/20 hover:border-blue-500/50 transition-all duration-300" style={{ animationDelay: '100ms' }}>
            <CardContent className="pt-6 text-center">
              <div className="bg-gradient-to-br from-blue-500/20 to-blue-500/5 p-4 rounded-xl inline-block mb-4 group-hover:scale-110 transition-transform shadow-lg group-hover:shadow-blue-500/50">
                <BookOpen className="w-10 h-10 text-blue-500 group-hover:animate-pulse" />
              </div>
              <h3 className="text-lg font-semibold mb-2 group-hover:text-blue-500 transition-colors">Concept-First Learning</h3>
              <p className="text-sm text-muted-foreground leading-relaxed">
                Understand concepts before jargon with our proven teaching approach
              </p>
            </CardContent>
          </Card>
          <Card hover className="animate-fade-in-up shine group border-green-500/20 hover:border-green-500/50 transition-all duration-300" style={{ animationDelay: '200ms' }}>
            <CardContent className="pt-6 text-center">
              <div className="bg-gradient-to-br from-green-500/20 to-green-500/5 p-4 rounded-xl inline-block mb-4 group-hover:scale-110 transition-transform shadow-lg group-hover:shadow-green-500/50">
                <Zap className="w-10 h-10 text-green-500 group-hover:animate-pulse" />
              </div>
              <h3 className="text-lg font-semibold mb-2 group-hover:text-green-500 transition-colors">Instant Execution</h3>
              <p className="text-sm text-muted-foreground leading-relaxed">
                Write and run code directly in your browser. No setup required.
              </p>
            </CardContent>
          </Card>
          <Card hover className="animate-fade-in-up shine group border-purple-500/20 hover:border-purple-500/50 transition-all duration-300" style={{ animationDelay: '300ms' }}>
            <CardContent className="pt-6 text-center">
              <div className="bg-gradient-to-br from-purple-500/20 to-purple-500/5 p-4 rounded-xl inline-block mb-4 group-hover:scale-110 transition-transform shadow-lg group-hover:shadow-purple-500/50">
                <Award className="w-10 h-10 text-purple-500 group-hover:animate-pulse" />
              </div>
              <h3 className="text-lg font-semibold mb-2 group-hover:text-purple-500 transition-colors">Track Progress</h3>
              <p className="text-sm text-muted-foreground leading-relaxed">
                See your advancement across all languages with detailed analytics
              </p>
            </CardContent>
          </Card>
        </div>
      </section>

      {/* Language Selection */}
      <section className="container mx-auto px-4 py-16 md:py-24 relative z-10">
        <div className="text-center mb-12 animate-fade-in-up">
          <h3 className="text-3xl md:text-4xl font-bold mb-4 bg-gradient-to-r from-foreground to-foreground/60 bg-clip-text text-transparent">
            Choose Your Language
          </h3>
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
              className={`group relative block animate-fade-in-up focus:outline-none focus-visible:ring-2 focus-visible:ring-primary focus-visible:ring-offset-2 rounded-lg transition-all ${
                lang.comingSoon ? 'pointer-events-none' : ''
              }`}
              style={{ animationDelay: `${index * 50}ms` }}
              onClick={(e) => lang.comingSoon && e.preventDefault()}
              onKeyDown={(e) => {
                if (lang.comingSoon) {
                  e.preventDefault()
                  return
                }
                if (e.key === 'Enter' || e.key === ' ') {
                  e.preventDefault()
                  // React Router's Link will handle navigation
                  e.currentTarget.click()
                }
              }}
              tabIndex={lang.comingSoon ? -1 : 0}
              aria-disabled={lang.comingSoon}
              aria-label={`${lang.name} course - ${lang.lessons} lessons${lang.comingSoon ? ' (Coming Soon)' : ''}`}
            >
              <Card
                hover={!lang.comingSoon}
                className={`h-full transition-all duration-300 shine ${
                  lang.comingSoon
                    ? 'opacity-60 cursor-not-allowed'
                    : 'hover:border-primary/50 hover:shadow-2xl hover:shadow-primary/20 hover:-translate-y-2'
                }`}
              >
                <CardContent className="p-6">
                  {lang.comingSoon && (
                    <Badge variant="warning" className="absolute top-4 right-4 animate-pulse">
                      Coming Soon
                    </Badge>
                  )}
                  <div className="relative mb-4">
                    <div className="absolute inset-0 bg-gradient-to-r from-primary/20 to-purple-500/20 rounded-xl blur-lg opacity-0 group-hover:opacity-100 transition-opacity"></div>
                    <div
                      className={`relative w-16 h-16 ${lang.color} rounded-xl flex items-center justify-center shadow-lg transform group-hover:scale-110 group-hover:rotate-6 transition-all duration-300`}
                    >
                      <Code2 className="w-8 h-8 text-white" />
                    </div>
                  </div>
                  <h4 className="text-2xl font-bold mb-2 group-hover:bg-gradient-to-r group-hover:from-primary group-hover:to-purple-500 group-hover:bg-clip-text group-hover:text-transparent transition-all">
                    {lang.name}
                  </h4>
                  <p className="text-sm text-muted-foreground mb-4 line-clamp-2">
                    {lang.description}
                  </p>
                  <div className="flex items-center justify-between pt-4 border-t border-border/50">
                    <div className="flex items-center gap-2 text-muted-foreground group-hover:text-foreground transition-colors">
                      <BookOpen className="w-4 h-4" />
                      <span className="text-sm font-medium">{lang.lessons} Lessons</span>
                    </div>
                    {!lang.comingSoon && (
                      <CheckCircle2 className="w-5 h-5 text-green-500 group-hover:scale-125 transition-transform" />
                    )}
                  </div>
                  {!lang.comingSoon && (
                    <div className="mt-4">
                      <Button variant="outline" size="sm" className="w-full group-hover:bg-gradient-to-r group-hover:from-primary group-hover:to-purple-500 group-hover:text-white group-hover:border-transparent transition-all">
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

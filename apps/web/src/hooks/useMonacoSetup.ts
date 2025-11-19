import { useEffect } from 'react'
import { editor, languages } from 'monaco-editor'
import { customThemes, codeSnippets } from '../utils/monacoConfig'

/**
 * Hook to set up Monaco editor with custom themes and IntelliSense
 * Should be called once at app initialization
 */
export function useMonacoSetup() {
  useEffect(() => {
    // Define custom themes
    if (customThemes) {
      Object.entries(customThemes).forEach(([themeName, themeData]) => {
        editor.defineTheme(themeName, themeData)
      })
    }

    // Register code snippets for supported languages
    if (codeSnippets) {
      Object.entries(codeSnippets).forEach(([lang, snippets]) => {
        if (languages.registerCompletionItemProvider) {
          languages.registerCompletionItemProvider(lang, {
            provideCompletionItems: () => {
              return { suggestions: snippets }
            },
          })
        }
      })
    }

    // Configure language defaults
    configureLanguageDefaults()
  }, [])
}

/**
 * Configure default settings for each language
 */
function configureLanguageDefaults() {
  // TypeScript/JavaScript configuration
  if (languages.typescript) {
    languages.typescript.typescriptDefaults.setCompilerOptions({
      target: languages.typescript.ScriptTarget.ES2020,
      allowNonTsExtensions: true,
      moduleResolution: languages.typescript.ModuleResolutionKind.NodeJs,
      module: languages.typescript.ModuleKind.CommonJS,
      noEmit: true,
      esModuleInterop: true,
      allowJs: true,
    })

    languages.typescript.typescriptDefaults.setDiagnosticsOptions({
      noSemanticValidation: false,
      noSyntaxValidation: false,
    })

    languages.typescript.javascriptDefaults.setCompilerOptions({
      target: languages.typescript.ScriptTarget.ES2020,
      allowNonTsExtensions: true,
      allowJs: true,
    })

    languages.typescript.javascriptDefaults.setDiagnosticsOptions({
      noSemanticValidation: true,
      noSyntaxValidation: false,
    })

    // Set eager model sync for better IntelliSense
    languages.typescript.typescriptDefaults.setEagerModelSync(true)
    languages.typescript.javascriptDefaults.setEagerModelSync(true)
  }
}

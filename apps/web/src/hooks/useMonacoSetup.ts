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
    Object.entries(customThemes).forEach(([themeName, themeData]) => {
      editor.defineTheme(themeName, themeData)
    })

    // Register code snippets for each language
    Object.entries(codeSnippets).forEach(([language, snippets]) => {
      languages.registerCompletionItemProvider(language, {
        provideCompletionItems: (model, position) => {
          const word = model.getWordUntilPosition(position)
          const range = {
            startLineNumber: position.lineNumber,
            endLineNumber: position.lineNumber,
            startColumn: word.startColumn,
            endColumn: word.endColumn,
          }

          const suggestions = snippets.map((snippet) => ({
            label: snippet.label,
            kind: languages.CompletionItemKind.Snippet,
            insertText: snippet.insertText,
            insertTextRules: languages.CompletionItemInsertTextRule.InsertAsSnippet,
            documentation: snippet.documentation,
            range,
          }))

          return { suggestions }
        },
      })
    })

    // Configure language-specific settings
    configureLanguageDefaults()
  }, [])
}

/**
 * Configure default settings for each language
 */
function configureLanguageDefaults() {
  // TypeScript/JavaScript configuration
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

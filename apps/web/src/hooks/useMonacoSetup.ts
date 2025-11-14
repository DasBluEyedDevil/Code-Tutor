import { useEffect } from 'react'
// import { editor, languages } from 'monaco-editor'
// import { customThemes, codeSnippets } from '../utils/monacoConfig'

/**
 * Hook to set up Monaco editor with custom themes and IntelliSense
 * Should be called once at app initialization
 *
 * Note: Disabled for Electron build - Monaco setup is handled by @monaco-editor/react
 */
export function useMonacoSetup() {
  useEffect(() => {
    // Monaco setup disabled for now to fix build issues
    // This will be handled by the Monaco editor component itself

    // TODO: Re-enable monaco configuration when needed
    // See utils/monacoConfig.ts for theme and snippet definitions
  }, [])
}

/**
 * Configure default settings for each language
 * Note: Currently disabled for Electron build
 */
function configureLanguageDefaults() {
  // Disabled for now
  return

  /* Original code commented out
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
  */
}

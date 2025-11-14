import type { editor } from 'monaco-editor'
import type { EditorPreferences } from '../stores/preferencesStore'

/**
 * Enhanced Monaco editor configuration
 * Provides optimized settings for code learning environment
 */

export interface EditorConfig {
  language: string
  readOnly?: boolean
  compact?: boolean
  userPreferences?: EditorPreferences
}

/**
 * Get enhanced Monaco editor options
 */
export function getEditorOptions(config: EditorConfig): editor.IStandaloneEditorConstructionOptions {
  const { language, readOnly = false, compact = false, userPreferences } = config

  const baseOptions: editor.IStandaloneEditorConstructionOptions = {
    // Basic settings - use user preferences if provided
    fontSize: userPreferences?.fontSize ?? (compact ? 13 : 14),
    fontFamily:
      userPreferences?.fontFamily ?? 'Fira Code, Consolas, Monaco, "Courier New", monospace',
    fontLigatures: true,
    lineNumbers: userPreferences?.lineNumbers ?? 'on',
    lineNumbersMinChars: compact ? 3 : 4,

    // Layout
    minimap: { enabled: userPreferences?.minimap ?? !compact },
    scrollBeyondLastLine: false,
    automaticLayout: true,
    padding: { top: compact ? 8 : 12, bottom: compact ? 8 : 12 },

    // Editing behavior
    readOnly,
    tabSize: userPreferences?.tabSize ?? 2,
    insertSpaces: true,
    detectIndentation: true,
    wordWrap: userPreferences?.wordWrap ?? 'on',
    wrappingStrategy: 'advanced',

    // IntelliSense & suggestions
    quickSuggestions: {
      other: !readOnly,
      comments: false,
      strings: !readOnly,
    },
    suggestOnTriggerCharacters: !readOnly,
    acceptSuggestionOnCommitCharacter: !readOnly,
    acceptSuggestionOnEnter: 'on',
    snippetSuggestions: readOnly ? 'none' : 'top',
    suggest: {
      showKeywords: true,
      showSnippets: !readOnly,
      showClasses: true,
      showFunctions: true,
      showVariables: true,
      showModules: true,
    },

    // Code actions & hints
    lightbulb: {
      enabled: !readOnly,
    },
    parameterHints: {
      enabled: !readOnly,
    },
    hover: {
      enabled: true,
      delay: 300,
    },

    // Formatting - use user preferences if provided
    formatOnPaste: userPreferences?.formatOnPaste ?? !readOnly,
    formatOnType: userPreferences?.formatOnType ?? !readOnly,

    // Scrolling & rendering
    smoothScrolling: true,
    cursorBlinking: 'smooth',
    cursorSmoothCaretAnimation: 'on',
    renderLineHighlight: 'all',
    renderWhitespace: 'selection',

    // Bracket matching & folding
    matchBrackets: 'always',
    bracketPairColorization: {
      enabled: true,
    },
    folding: true,
    foldingStrategy: 'auto',

    // Selection & find
    selectOnLineNumbers: true,
    roundedSelection: true,
    find: {
      seedSearchStringFromSelection: 'always',
      autoFindInSelection: 'never',
    },
  }

  // Language-specific overrides
  const languageOverrides = getLanguageSpecificOptions(language)

  return { ...baseOptions, ...languageOverrides }
}

/**
 * Get language-specific Monaco options
 */
function getLanguageSpecificOptions(language: string): Partial<editor.IStandaloneEditorConstructionOptions> {
  switch (language) {
    case 'python':
      return {
        tabSize: 4,
        insertSpaces: true,
      }
    case 'java':
    case 'kotlin':
    case 'csharp':
      return {
        tabSize: 4,
        insertSpaces: true,
      }
    case 'javascript':
    case 'typescript':
      return {
        tabSize: 2,
        insertSpaces: true,
      }
    case 'rust':
      return {
        tabSize: 4,
        insertSpaces: true,
      }
    default:
      return {}
  }
}

/**
 * Custom theme definitions for Monaco editor
 */
export const customThemes = {
  'code-tutor-light': {
    base: 'vs' as const,
    inherit: true,
    rules: [
      { token: 'comment', foreground: '6a737d', fontStyle: 'italic' },
      { token: 'keyword', foreground: 'd73a49', fontStyle: 'bold' },
      { token: 'string', foreground: '032f62' },
      { token: 'number', foreground: '005cc5' },
      { token: 'type', foreground: '6f42c1' },
      { token: 'function', foreground: '6f42c1' },
      { token: 'variable', foreground: 'e36209' },
    ],
    colors: {
      'editor.background': '#ffffff',
      'editor.foreground': '#24292e',
      'editor.lineHighlightBackground': '#f6f8fa',
      'editorLineNumber.foreground': '#959da5',
      'editorCursor.foreground': '#044289',
    },
  },
  'code-tutor-dark': {
    base: 'vs-dark' as const,
    inherit: true,
    rules: [
      { token: 'comment', foreground: '8b949e', fontStyle: 'italic' },
      { token: 'keyword', foreground: 'ff7b72', fontStyle: 'bold' },
      { token: 'string', foreground: 'a5d6ff' },
      { token: 'number', foreground: '79c0ff' },
      { token: 'type', foreground: 'd2a8ff' },
      { token: 'function', foreground: 'd2a8ff' },
      { token: 'variable', foreground: 'ffa657' },
    ],
    colors: {
      'editor.background': '#0d1117',
      'editor.foreground': '#c9d1d9',
      'editor.lineHighlightBackground': '#161b22',
      'editorLineNumber.foreground': '#6e7681',
      'editorCursor.foreground': '#58a6ff',
    },
  },
}

/**
 * Common code snippets for different languages
 */
export const codeSnippets = {
  python: [
    {
      label: 'for',
      insertText: 'for ${1:item} in ${2:items}:\n    ${0:pass}',
      documentation: 'For loop',
    },
    {
      label: 'if',
      insertText: 'if ${1:condition}:\n    ${0:pass}',
      documentation: 'If statement',
    },
    {
      label: 'def',
      insertText: 'def ${1:function_name}(${2:params}):\n    ${0:pass}',
      documentation: 'Function definition',
    },
    {
      label: 'class',
      insertText: 'class ${1:ClassName}:\n    def __init__(self${2:, params}):\n        ${0:pass}',
      documentation: 'Class definition',
    },
  ],
  java: [
    {
      label: 'psvm',
      insertText: 'public static void main(String[] args) {\n    ${0}\n}',
      documentation: 'Main method',
    },
    {
      label: 'sout',
      insertText: 'System.out.println(${0});',
      documentation: 'Print to console',
    },
    {
      label: 'for',
      insertText: 'for (int ${1:i} = 0; ${1:i} < ${2:length}; ${1:i}++) {\n    ${0}\n}',
      documentation: 'For loop',
    },
  ],
  javascript: [
    {
      label: 'log',
      insertText: 'console.log(${0});',
      documentation: 'Log to console',
    },
    {
      label: 'func',
      insertText: 'function ${1:name}(${2:params}) {\n    ${0}\n}',
      documentation: 'Function declaration',
    },
    {
      label: 'arrow',
      insertText: 'const ${1:name} = (${2:params}) => {\n    ${0}\n}',
      documentation: 'Arrow function',
    },
  ],
  rust: [
    {
      label: 'fn',
      insertText: 'fn ${1:name}(${2:params}) -> ${3:ReturnType} {\n    ${0}\n}',
      documentation: 'Function definition',
    },
    {
      label: 'println',
      insertText: 'println!("${0}");',
      documentation: 'Print to console',
    },
  ],
}

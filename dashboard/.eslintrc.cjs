module.exports = {
    root: true,
    env: { browser: true, es2020: true },
    extends: [
        'eslint:recommended',
        'plugin:react/recommended',
        'plugin:react-hooks/recommended',
        'plugin:react-refresh/recommended'
    ],
    parserOptions: { ecmaVersion: 2020, sourceType: 'module' },
    settings: { react: { version: 'detect' } },
    rules: {
        'react/jsx-no-target-blank': 'off',
        'react-refresh/only-export-components': [
            'warn',
            { allowConstantExport: true },
        ],
    },
    ignorePatterns: ['dist', '.eslintrc.cjs'],
};

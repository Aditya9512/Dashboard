module.exports = {
    root: true,
    env: { browser: true, es2020: true },
    extends: [
        'react-app', // Use this if you are using create-react-app
        'react-app/jest', // Use this if you have testing setup
    ],
    ignorePatterns: ['dist', '.eslintrc.cjs'],
    parserOptions: { ecmaVersion: 'latest', sourceType: 'module' },
    settings: { react: { version: '18.2' } },
    rules: {
        'react/jsx-no-target-blank': 'off',
        'react-refresh/only-export-components': [
            'warn',
            { allowConstantExport: true },
        ],
    },
};

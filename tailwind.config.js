const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
	purge: {
		enabled: process.env.NODE_ENV === 'production',
		content: ['./**/*.html', './**/*.cshtml', './**/*.cs']
	},
	darkMode: false,
	theme: {
		extend: {
			fontFamily: {
				sans: ['Inter var', 'Arial', 'Helvetica', 'sans-serif'],
				mono: ['JetBrains Mono', ...defaultTheme.fontFamily.mono],
			},
			typography: {
				DEFAULT: {
					css: {
						'pre code::before': {
							content: 'none',
						},
						'pre code::after': {
							content: 'none',
						},
					},
				},
			}
		},
	},
	variants: {
		extend: {
			outline: ['hover', 'active'],
			ringColor: ['hover', 'active'],
			ringOffsetColor: ['hover', 'active'],
			ringOffsetWidth: ['hover', 'active'],
			ringOpacity: ['hover', 'active'],
			ringWidth: ['hover', 'active'],
		},
	},
	plugins: [
		require('@tailwindcss/typography')
	],
}

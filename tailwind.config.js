const defaultTheme = require('tailwindcss/defaultTheme')

module.exports = {
	content: [
		'./src/**/*.html',
		'./src/**/*.cshtml',
		'./src/**/*.cs',
		'./src/**/*_controller.js',
	],
	theme: {
		extend: {
			fontFamily: {
				sans: ['Inter var', 'Arial', 'Helvetica', 'sans-serif'],
			},
		},
	},
	plugins: [
		require('@tailwindcss/forms'),
		require('@tailwindcss/typography'),
	],
}

// --light-blue-100: #0037ff;
// --light-blue-75: #0037ff;
// --light-blue-50: #b3c3ff;
// --light-blue-25: #e6ebff;
// --light-blue-10: #f0f3ff;
// --light-blue-6: #f0f3ff;
// --dark-blue-75: #080736;
// --gold: #ffc105;
// --red: #f33030;
// --green: #4cae4f;
// --indigo: #693ab6;
// --cyan: #00bdd6;
// --blue: #2094f3;
// --orange: #f90;
// --dark-blue-100: #080736;
// --dark-blue-50: #9c9cb0;
// --dark-blue-25: #9c9cb0;
// --grey-50: #ededed;
// --grey-25: #f5f5f5;
// --paper: #fff;
// --dark-paper: #fafafa;
// --white-40b: #999;
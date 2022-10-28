const path = require('path');

module.exports = {
	entry: './src/index.js',
	output: {
		filename: 'main.js',
		path: path.resolve(__dirname, './src/WebApp/wwwroot/dist/')
	},
	mode: 'production',
	watchOptions: {
		ignored: '**/node_modules',
	},
	devtool: 'source-map',
	module: {
		rules: [
			{
				test: /\.js$/,
				exclude: [
					/node_modules/
				],
				use: [
					{
						loader: "babel-loader",
						options: {
							presets: ['@babel/preset-env'],
							plugins: ['@babel/plugin-proposal-class-properties', '@babel/plugin-transform-runtime']
						}
					}
				]
			}
		]
	}
};

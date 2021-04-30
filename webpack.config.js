const path = require('path');
const ExtractTextPlugin = require('extract-text-webpack-plugin');
const WriteFileWebpackPlugin = require('write-file-webpack-plugin');

module.exports = {
	entry: './src/index.js',
	output: {
		filename: 'main.js',
		path: path.resolve(__dirname, './src/WebApp/wwwroot/')
	},
	mode: 'production', ////process.env.NODE_ENV,
	devtool: 'source-map',
	module: {
		rules: [
			{
				test: /\.js$/,
				exclude: [
				  /node_modules/
				],
				use: [
				  { loader: "babel-loader" }
				]
			},
			{
				test: /\.css$/,
				use: ExtractTextPlugin.extract({
					fallback: 'style-loader',
					use: [
						{
							loader: 'css-loader',
							options: { importLoaders: 1 }
						},
						'postcss-loader'
					]
				})
			},
			{
				test: /\.(png|woff|woff2|eot|ttf|svg)$/,
				use: [
					{
						loader: 'url-loader',
						options: {
							limit: 10000,
						},
					},
				],
			}
		]
	},
	plugins: [
		new ExtractTextPlugin('styles.css', {
			disable: false
		}),
		new WriteFileWebpackPlugin({
			// Write only files that have ".css" extension.
			test: /\.(css|png|woff|woff2|eot|ttf|svg)$/,
			useHashIndex: true
		}),
	]
};

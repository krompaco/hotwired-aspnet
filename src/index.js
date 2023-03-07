import * as Turbo from "@hotwired/turbo"
import { Application } from "@hotwired/stimulus"
import { definitionsFromContext } from "@hotwired/stimulus-webpack-helpers"
import './signalr_turbo_stream_element'

const application = Application.start()
const context = require.context("./stimulus_controllers", true, /\.js$/)
application.load(definitionsFromContext(context))

application.debug = true;
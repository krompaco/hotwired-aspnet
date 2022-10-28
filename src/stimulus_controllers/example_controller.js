import { Controller } from "@hotwired/stimulus"
import { connectStreamSource, disconnectStreamSource } from '@hotwired/turbo';

export default class extends Controller {
  connect() {
    this.element.textContent = "Text from Stimulus controller.";

    // NOTE: The web socket parts are highly experimental both in JS and WebApp
    this.source = new WebSocket('wss://localhost:5001/streamtesthandler');
    connectStreamSource(this.source);
  }

  disconnect() {
    if (this.source) {
      disconnectStreamSource(this.source);
      this.source.close();
      this.source = null;
    }
  }
}
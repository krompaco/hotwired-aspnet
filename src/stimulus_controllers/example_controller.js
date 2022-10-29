import { Controller } from "@hotwired/stimulus"
import { connectStreamSource, disconnectStreamSource } from '@hotwired/turbo';

export default class extends Controller {
  connect() {
    this.element.textContent = "Text from Stimulus controller.";

    // NOTE: The web socket parts are highly experimental both in JS and WebApp
    var origin = window.location.origin.replace('https://', 'wss://').replace('http://', 'ws://');
    this.source = new WebSocket(origin + '/streamtesthandler');
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
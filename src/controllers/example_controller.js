import { Controller } from "@hotwired/stimulus"
import { connectStreamSource, disconnectStreamSource } from '@hotwired/turbo';

export default class extends Controller {
  connect() {
    this.element.textContent = "It works from Stimulus! Test.";

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
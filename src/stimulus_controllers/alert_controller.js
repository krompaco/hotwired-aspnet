import { Controller } from '@hotwired/stimulus'

// From https://github.com/excid3/tailwindcss-stimulus-components
export default class extends Controller {
  static values = {
    dismissAfter: Number,
    showDelay: { type: Number, default: 200 },
    removeDelay: { type: Number, default: 1100 }
  }
  static classes = ["show", "hide"]

  initialize() {
    this.hide()
  }

  connect() {
    setTimeout(() => {
      this.show()
    }, this.showDelayValue)

    // Auto dismiss if defined
    if (this.hasDismissAfterValue) {
      setTimeout(() => {
        this.close()
      }, this.dismissAfterValue)
    }
  }

  close() {
    this.hide()

    setTimeout(() => {
      this.element.remove()
    }, this.removeDelayValue)
  }

  show() {
    this.element.classList.add(...this.showClasses)
    this.element.classList.remove(...this.hideClasses)
  }

  hide() {
    this.element.classList.add(...this.hideClasses)
    this.element.classList.remove(...this.showClasses)
  }
}
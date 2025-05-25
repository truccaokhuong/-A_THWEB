// Wait for the DOM to be fully loaded
document.addEventListener("DOMContentLoaded", () => {
  // Add to cart functionality
  const addToCartButtons = document.querySelectorAll(".btn-orange")

  addToCartButtons.forEach((button) => {
    button.addEventListener("click", function () {
      // Get product information
      const card = this.closest(".card")
      const productName = card.querySelector(".card-title").textContent
      const productPrice = card.querySelector(".card-text").textContent

      // Show add to cart notification
      showNotification(`${productName} added to cart!`)

      // Update cart count
      updateCartCount()
    })
  })

  // Function to show notification
  function showNotification(message) {
    // Create notification element
    const notification = document.createElement("div")
    notification.className = "position-fixed bottom-0 end-0 p-3"
    notification.style.zIndex = "5"

    notification.innerHTML = `
            <div class="toast show" role="alert" aria-live="assertive" aria-atomic="true">
                <div class="toast-header bg-orange text-white">
                    <strong class="me-auto">Shopping Cart</strong>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="toast" aria-label="Close"></button>
                </div>
                <div class="toast-body">
                    ${message}
                </div>
            </div>
        `

    // Add to document
    document.body.appendChild(notification)

    // Remove after 3 seconds
    setTimeout(() => {
      notification.remove()
    }, 3000)
  }

  // Function to update cart count
  function updateCartCount() {
    const cartBadge = document.querySelector(".fa-shopping-cart + .badge")
    const currentCount = Number.parseInt(cartBadge.textContent)
    cartBadge.textContent = currentCount + 1
  }

  // Search functionality
  const searchInput = document.querySelector('input[type="text"]')
  const searchButton = searchInput.nextElementSibling

  searchButton.addEventListener("click", () => {
    const searchTerm = searchInput.value.trim()
    if (searchTerm) {
      // In a real application, this would redirect to search results
      alert(`Searching for: ${searchTerm}`)
    }
  })

  // Allow search on Enter key press
  searchInput.addEventListener("keypress", (e) => {
    if (e.key === "Enter") {
      searchButton.click()
    }
  })

  // Newsletter subscription
  const subscribeButton = document.querySelector(".card-body .btn-orange")
  const emailInput = document.querySelector('.card-body input[type="email"]')

  subscribeButton.addEventListener("click", () => {
    const email = emailInput.value.trim()
    if (email && isValidEmail(email)) {
      // In a real application, this would submit the email to a server
      alert(`Thank you for subscribing with: ${email}`)
      emailInput.value = ""
    } else {
      alert("Please enter a valid email address")
    }
  })

  // Email validation function
  function isValidEmail(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
    return regex.test(email)
  }

  // Initialize any Bootstrap tooltips
  const tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
  const tooltipList = tooltipTriggerList.map((tooltipTriggerEl) => new bootstrap.Tooltip(tooltipTriggerEl))

  // Initialize the carousel with custom settings
  const mainCarousel = document.getElementById("mainCarousel")
  if (mainCarousel) {
    const carousel = new bootstrap.Carousel(mainCarousel, {
      interval: 5000, // Change slides every 5 seconds
      wrap: true, // Continuous loop
      keyboard: true, // Allow keyboard navigation
    })

    // Pause carousel on hover (optional)
    mainCarousel.addEventListener("mouseenter", () => {
      carousel.pause()
    })

    mainCarousel.addEventListener("mouseleave", () => {
      carousel.cycle()
    })
  }
})

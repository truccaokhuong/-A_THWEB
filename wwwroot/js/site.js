// EzBooking Site JavaScript

$(document).ready(function() {
  // Initialize components
  initializeComponents();
  
  // Add loading states
  addLoadingStates();
  
  // Initialize tooltips and popovers
  initializeBootstrapComponents();
  
  // Add smooth animations
  addAnimations();
});

// Initialize various components
function initializeComponents() {
  // Add click handlers for external links
  $('a[href^="http"]').attr('target', '_blank').attr('rel', 'noopener noreferrer');
  
  // Add loading state to forms
  $('form').on('submit', function() {
      const submitBtn = $(this).find('button[type="submit"]');
      if (submitBtn.length) {
          submitBtn.prop('disabled', true).html('<span class="loading me-2"></span>Loading...');
      }
  });
  
  // Back to top button
  if ($('#back-to-top').length === 0) {
      $('body').append('<button id="back-to-top" class="btn btn-primary position-fixed" style="bottom: 20px; right: 20px; z-index: 1000; display: none; border-radius: 50%; width: 50px; height: 50px;"><i class="fas fa-arrow-up"></i></button>');
  }
  
  $(window).scroll(function() {
      if ($(this).scrollTop() > 100) {
          $('#back-to-top').fadeIn();
      } else {
          $('#back-to-top').fadeOut();
      }
  });
  
  $('#back-to-top').click(function() {
      $('html, body').animate({scrollTop: 0}, 800);
      return false;
  });
}

// Add loading states to buttons and links
function addLoadingStates() {
  // Add loading state to navigation buttons
  $('.nav-link, .btn').not('.no-loading').on('click', function(e) {
      const $this = $(this);
      
      // Skip if it's a dropdown, hash link, javascript void, or has no-loading class
      if ($this.hasClass('dropdown-toggle') || 
          $this.hasClass('no-loading') ||
          $this.attr('href') === '#' || 
          $this.attr('href') === 'javascript:void(0)' ||
          $this.attr('data-toggle') === 'modal' ||
          $this.attr('data-dismiss') === 'modal') {
          return;
      }
      
      // Only show loading for actual page navigation
      const href = $this.attr('href');
      if (href && href.length > 1 && !href.startsWith('#') && !href.startsWith('javascript:')) {
          showLoadingOverlay();
      }
  });
}

// Initialize Bootstrap components
function initializeBootstrapComponents() {
  // Initialize tooltips
  var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
  var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
      return new bootstrap.Tooltip(tooltipTriggerEl);
  });
  
  // Initialize popovers
  var popoverTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="popover"]'));
  var popoverList = popoverTriggerList.map(function (popoverTriggerEl) {
      return new bootstrap.Popover(popoverTriggerEl);
  });
}

// Add smooth animations
function addAnimations() {
  // Intersection Observer for fade-in animations
  if ('IntersectionObserver' in window) {
      const observer = new IntersectionObserver((entries) => {
          entries.forEach(entry => {
              if (entry.isIntersecting) {
                  entry.target.classList.add('fade-in');
              }
          });
      });
      
      // Observe elements that should fade in
      document.querySelectorAll('.hotel-card, .destination-card, .offer-card, .stat-item').forEach(el => {
          observer.observe(el);
      });
  }
}

// Show loading overlay
function showLoadingOverlay() {
  if ($('#loading-overlay').length === 0) {
      $('body').append(`
          <div id="loading-overlay" class="loading-overlay">
              <div class="loading-spinner"></div>
          </div>
      `);
  }
  $('#loading-overlay').show();
}

// Hide loading overlay
function hideLoadingOverlay() {
  $('#loading-overlay').hide().remove();
  $('.loading-spinner').remove();
  // Also remove any stuck loading overlays with different IDs
  $('[id*="loading"], [class*="loading-overlay"]').remove();
}

// Show toast notification
function showToast(message, type = 'info') {
  const toastId = 'toast-' + Date.now();
  const toastHtml = `
      <div id="${toastId}" class="toast" role="alert" aria-live="assertive" aria-atomic="true">
          <div class="toast-header">
              <i class="fas fa-${getToastIcon(type)} me-2 text-${type}"></i>
              <strong class="me-auto">EzBooking</strong>
              <button type="button" class="btn-close" data-bs-dismiss="toast"></button>
          </div>
          <div class="toast-body">
              ${message}
          </div>
      </div>
  `;
  
  // Create toast container if it doesn't exist
  if ($('.toast-container').length === 0) {
      $('body').append('<div class="toast-container"></div>');
  }
  
  $('.toast-container').append(toastHtml);
  const toast = new bootstrap.Toast(document.getElementById(toastId));
  toast.show();
  
  // Auto remove after hide
  document.getElementById(toastId).addEventListener('hidden.bs.toast', function() {
      $(this).remove();
  });
}

// Get appropriate icon for toast type
function getToastIcon(type) {
  switch(type) {
      case 'success': return 'check-circle';
      case 'danger': case 'error': return 'exclamation-circle';
      case 'warning': return 'exclamation-triangle';
      default: return 'info-circle';
  }
}

// Format currency
function formatCurrency(amount, currency = 'USD') {
  const formatter = new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: currency,
      minimumFractionDigits: 0,
      maximumFractionDigits: 2
  });
  return formatter.format(amount);
}

// Format date
function formatDate(date, options = {}) {
  const defaultOptions = {
      year: 'numeric',
      month: 'short',
      day: 'numeric'
  };
  const formatOptions = { ...defaultOptions, ...options };
  
  return new Intl.DateTimeFormat('en-US', formatOptions).format(new Date(date));
}

// Debounce function for search
function debounce(func, wait, immediate) {
  let timeout;
  return function executedFunction() {
      const context = this;
      const args = arguments;
      const later = function() {
          timeout = null;
          if (!immediate) func.apply(context, args);
      };
      const callNow = immediate && !timeout;
      clearTimeout(timeout);
      timeout = setTimeout(later, wait);
      if (callNow) func.apply(context, args);
  };
}

// Lazy load images
function lazyLoadImages() {
  if ('IntersectionObserver' in window) {
      const imageObserver = new IntersectionObserver((entries, observer) => {
          entries.forEach(entry => {
              if (entry.isIntersecting) {
                  const img = entry.target;
                  img.src = img.dataset.src;
                  img.classList.remove('lazy');
                  imageObserver.unobserve(img);
              }
          });
      });
      
      document.querySelectorAll('img[data-src]').forEach(img => {
          imageObserver.observe(img);
      });
  }
}

// Handle image loading errors
$(document).on('error', 'img', function() {
  const $img = $(this);
  if (!$img.hasClass('error-handled')) {
      $img.addClass('error-handled');
      
      // Try to load a default image
      if ($img.hasClass('hotel-image')) {
          $img.attr('src', '/images/hotels/default-hotel.jpg');
      } else if ($img.hasClass('destination-image')) {
          $img.attr('src', '/images/destinations/default-destination.jpg');
      } else {
          $img.attr('src', '/images/default-image.jpg');
      }
  }
});

// Local storage helpers
const Storage = {
  set: function(key, value) {
      try {
          localStorage.setItem(key, JSON.stringify(value));
      } catch(e) {
          console.warn('Could not save to localStorage:', e);
      }
  },
  
  get: function(key) {
      try {
          const item = localStorage.getItem(key);
          return item ? JSON.parse(item) : null;
      } catch(e) {
          console.warn('Could not read from localStorage:', e);
          return null;
      }
  },
  
  remove: function(key) {
      try {
          localStorage.removeItem(key);
      } catch(e) {
          console.warn('Could not remove from localStorage:', e);
      }
  }
};

// Search history
const SearchHistory = {
  add: function(searchTerm) {
      let history = this.get();
      history = history.filter(item => item !== searchTerm);
      history.unshift(searchTerm);
      
      // Keep only last 10 searches
      if (history.length > 10) {
          history = history.slice(0, 10);
      }
      
      Storage.set('searchHistory', history);
  },
  
  get: function() {
      return Storage.get('searchHistory') || [];
  },
  
  clear: function() {
      Storage.remove('searchHistory');
  }
};

// Favorites
const Favorites = {
  add: function(hotelId) {
      let favorites = this.get();
      if (!favorites.includes(hotelId)) {
          favorites.push(hotelId);
          Storage.set('favorites', favorites);
          showToast('Hotel added to favorites!', 'success');
      }
  },
  
  remove: function(hotelId) {
      let favorites = this.get();
      favorites = favorites.filter(id => id !== hotelId);
      Storage.set('favorites', favorites);
      showToast('Hotel removed from favorites.', 'info');
  },
  
  get: function() {
      return Storage.get('favorites') || [];
  },
  
  isFavorite: function(hotelId) {
      return this.get().includes(hotelId);
  }
};

// Recently viewed
const RecentlyViewed = {
  add: function(hotel) {
      let recent = this.get();
      recent = recent.filter(item => item.id !== hotel.id);
      recent.unshift(hotel);
      
      // Keep only last 5 viewed
      if (recent.length > 5) {
          recent = recent.slice(0, 5);
      }
      
      Storage.set('recentlyViewed', recent);
  },
  
  get: function() {
      return Storage.get('recentlyViewed') || [];
  }
};

// Expose functions globally
window.EzBooking = {
  showToast,
  formatCurrency,
  formatDate,
  Storage,
  SearchHistory,
  Favorites,
  RecentlyViewed,
  showLoadingOverlay,
  hideLoadingOverlay
};

// Hide loading overlay when page is fully loaded
$(document).ready(function() {
  // Immediately hide any existing loading overlays
  hideLoadingOverlay();
  $('#loading-overlay').remove();
  $('.loading-spinner').remove();
});

$(window).on('load', function() {
  // Double check - hide loading overlay when page is fully loaded
  hideLoadingOverlay();
  $('#loading-overlay').remove();
  $('.loading-spinner').remove();
});

// Handle page unload - only for actual navigation, not for modal/popup actions
$(window).on('beforeunload', function(e) {
  // Only show loading if we're actually navigating away
  // Don't show for modal actions, AJAX calls, etc.
  if (!e.target.activeElement || 
      !e.target.activeElement.hasAttribute('data-toggle') &&
      !e.target.activeElement.hasAttribute('data-dismiss')) {
      showLoadingOverlay();
  }
});
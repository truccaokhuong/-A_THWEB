const AttractionsService = {
  // Get all attractions with pagination and filtering
  async getAllAttractions(page = 1, limit = 10, filters = {}) {
    try {
      const queryParams = new URLSearchParams({
        page: page.toString(),
        limit: limit.toString(),
        ...filters
      });

      const response = await fetch(`/api/attractions?${queryParams}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return await response.json();
    } catch (error) {
      console.error('Error fetching attractions:', error);
      throw error;
    }
  },

  // Get attraction by ID
  async getAttractionById(id) {
    try {
      const response = await fetch(`/api/attractions/${id}`);
      if (!response.ok) {
        if (response.status === 404) {
          throw new Error('Attraction not found');
        }
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return await response.json();
    } catch (error) {
      console.error('Error fetching attraction:', error);
      throw error;
    }
  },

  // Create new attraction
  async createAttraction(attractionData) {
    try {
      const response = await fetch('/api/attractions', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(attractionData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Failed to create attraction');
      }

      return await response.json();
    } catch (error) {
      console.error('Error creating attraction:', error);
      throw error;
    }
  },

  // Update attraction
  async updateAttraction(id, updateData) {
    try {
      const response = await fetch(`/api/attractions/${id}`, {
        method: 'PUT',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(updateData),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Failed to update attraction');
      }

      return await response.json();
    } catch (error) {
      console.error('Error updating attraction:', error);
      throw error;
    }
  },

  // Delete attraction
  async deleteAttraction(id) {
    try {
      const response = await fetch(`/api/attractions/${id}`, {
        method: 'DELETE',
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Failed to delete attraction');
      }

      return await response.json();
    } catch (error) {
      console.error('Error deleting attraction:', error);
      throw error;
    }
  },

  // Search attractions
  async searchAttractions(searchTerm, filters = {}) {
    try {
      const queryParams = new URLSearchParams({
        q: searchTerm,
        ...filters
      });

      const response = await fetch(`/api/attractions/search?${queryParams}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return await response.json();
    } catch (error) {
      console.error('Error searching attractions:', error);
      throw error;
    }
  },

  // Get attractions by category
  async getAttractionsByCategory(category) {
    try {
      const response = await fetch(`/api/attractions/category/${category}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return await response.json();
    } catch (error) {
      console.error('Error fetching attractions by category:', error);
      throw error;
    }
  },

  // Get nearby attractions
  async getNearbyAttractions(latitude, longitude, radius = 10) {
    try {
      const queryParams = new URLSearchParams({
        lat: latitude.toString(),
        lng: longitude.toString(),
        radius: radius.toString()
      });

      const response = await fetch(`/api/attractions/nearby?${queryParams}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return await response.json();
    } catch (error) {
      console.error('Error fetching nearby attractions:', error);
      throw error;
    }
  },

  // Upload attraction images
  async uploadImages(attractionId, imageFiles) {
    try {
      const formData = new FormData();
      imageFiles.forEach((file, index) => {
        formData.append(`images`, file);
      });

      const response = await fetch(`/api/attractions/${attractionId}/images`, {
        method: 'POST',
        body: formData,
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Failed to upload images');
      }

      return await response.json();
    } catch (error) {
      console.error('Error uploading images:', error);
      throw error;
    }
  },

  // Get attraction statistics
  async getAttractionStats(id) {
    try {
      const response = await fetch(`/api/attractions/${id}/stats`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }
      
      return await response.json();
    } catch (error) {
      console.error('Error fetching attraction stats:', error);
      throw error;
    }
  },

  // Rate attraction
  async rateAttraction(id, rating, review = '') {
    try {
      const response = await fetch(`/api/attractions/${id}/rate`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ rating, review }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.message || 'Failed to rate attraction');
      }

      return await response.json();
    } catch (error) {
      console.error('Error rating attraction:', error);
      throw error;
    }
  }
};

export default AttractionsService; 
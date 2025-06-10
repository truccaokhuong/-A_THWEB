// Tab switching
function switchTab(tab) {
    // Update tab buttons
    document.querySelectorAll('.tab-btn').forEach(btn => btn.classList.remove('active'));
    event.target.classList.add('active');
    
    // Update tab content
    document.querySelectorAll('.tab-content').forEach(content => content.classList.remove('active'));
    document.getElementById(tab + '-tab').classList.add('active');
}

// Password toggle
function togglePassword(inputId) {
    const input = document.getElementById(inputId);
    const icon = event.target.closest('button').querySelector('i');
    
    if (input.type === 'password') {
        input.type = 'text';
        icon.classList.remove('fa-eye');
        icon.classList.add('fa-eye-slash');
    } else {
        input.type = 'password';
        icon.classList.remove('fa-eye-slash');
        icon.classList.add('fa-eye');
    }
}

// Password strength checker
function checkPasswordStrength(password) {
    let strength = 0;
    
    if (password.length >= 8) strength++;
    if (/[a-z]/.test(password)) strength++;
    if (/[A-Z]/.test(password)) strength++;
    if (/[0-9]/.test(password)) strength++;
    if (/[^A-Za-z0-9]/.test(password)) strength++;
    
    return strength;
}

// Update password strength meter
document.getElementById('registerPassword').addEventListener('input', function() {
    const password = this.value;
    const strength = checkPasswordStrength(password);
    const fill = document.getElementById('strengthFill');
    
    fill.className = 'strength-fill';
    
    if (strength === 0) {
        fill.style.width = '0%';
    } else if (strength <= 2) {
        fill.classList.add('strength-weak');
    } else if (strength <= 3) {
        fill.classList.add('strength-fair');
    } else if (strength <= 4) {
        fill.classList.add('strength-good');
    } else {
        fill.classList.add('strength-strong');
    }
});

// Form validation
function validateEmail(email) {
    const re = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return re.test(email);
}

function showError(inputId, message) {
    const input = document.getElementById(inputId);
    const error = document.getElementById(inputId + 'Error');
    
    input.classList.add('is-invalid');
    input.classList.remove('is-valid');
    error.textContent = message;
}

function showSuccess(inputId) {
    const input = document.getElementById(inputId);
    const error = document.getElementById(inputId + 'Error');
    
    input.classList.add('is-valid');
    input.classList.remove('is-invalid');
    error.textContent = '';
}

function clearValidation(inputId) {
    const input = document.getElementById(inputId);
    const error = document.getElementById(inputId + 'Error');
    
    input.classList.remove('is-valid', 'is-invalid');
    error.textContent = '';
}

function showGeneralError(formId, message) {
    const errorElement = document.getElementById(formId + 'GeneralError');
    if (errorElement) {
        errorElement.textContent = message;
        errorElement.style.display = 'block'; // Make sure it's visible
    }
}

function clearGeneralError(formId) {
    const errorElement = document.getElementById(formId + 'GeneralError');
    if (errorElement) {
        errorElement.textContent = '';
        errorElement.style.display = 'none'; // Hide it when empty
    }
}

function showGeneralSuccess(formId, message) {
    const successElement = document.getElementById(formId + 'GeneralSuccess');
    if (successElement) {
        successElement.textContent = message;
        successElement.style.display = 'block'; // Make sure it's visible
         // Hide error message if success is shown
        clearGeneralError(formId);
    }
}

function clearGeneralSuccess(formId) {
     const successElement = document.getElementById(formId + 'GeneralSuccess');
     if (successElement) {
         successElement.textContent = '';
         successElement.style.display = 'none'; // Hide it when empty
     }
}

// Login form submission
document.getElementById('loginForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    
    // Clear previous errors
    clearGeneralError('loginForm');
    clearValidation('loginEmail');
    clearValidation('loginPassword');
    
    const emailInput = document.getElementById('loginEmail');
    const passwordInput = document.getElementById('loginPassword');
    const rememberMeInput = document.getElementById('rememberMe');
    
    const email = emailInput.value;
    const password = passwordInput.value;
    const rememberMe = rememberMeInput.checked;
    
    let isValid = true;
    
    // Validate email
    if (!email) {
        showError('loginEmail', 'Email is required');
        isValid = false;
    } else if (!validateEmail(email)) {
        showError('loginEmail', 'Please enter a valid email');
        isValid = false;
    } else {
        showSuccess('loginEmail');
    }
    
    // Validate password
    if (!password) {
        showError('loginPassword', 'Password is required');
        isValid = false;
    } else {
        showSuccess('loginPassword');
    }
    
    if (isValid) {
        const submitBtn = this.querySelector('button[type="submit"]');
        const originalBtnText = submitBtn.textContent;
        submitBtn.textContent = 'Signing in...';
        submitBtn.disabled = true;
          try {
            const response = await fetch('/Identity/Account/LoginJson', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, password, rememberMe })
            });
            
            if (response.ok) {
                // Redirect to home page or intended URL on success
                window.location.href = '/'; // Or get returnUrl from query string
            } else {
                const errorData = await response.json();
                if (errorData.errors) {
                    // Display specific errors from the server
                    for (const key in errorData.errors) {
                        if (errorData.errors.hasOwnProperty(key)) {
                            const errorMessages = errorData.errors[key];
                            const inputId = key.toLowerCase() === 'email' ? 'loginEmail' : (key.toLowerCase() === 'password' ? 'loginPassword' : null);
                            if (inputId) {
                                showError(inputId, errorMessages.join(' '));
                            } else {
                                // Handle other potential errors
                                showGeneralError('loginForm', errorMessages.join(' '));
                            }
                        }
                    }
                } else if (errorData.message) {
                    // Display general error message
                    showGeneralError('loginForm', errorData.message);
                } else {
                    showGeneralError('loginForm', 'Login failed. Please try again.');
                }
            }
        } catch (error) {
            console.error('Login error:', error);
            showGeneralError('loginForm', 'An error occurred during login. Please try again.');
        } finally {
            submitBtn.textContent = originalBtnText;
            submitBtn.disabled = false;
        }
    }
});

// Register form submission
document.getElementById('registerForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    
    // Clear previous errors and success messages
    clearGeneralError('registerForm');
    clearGeneralSuccess('registerForm');
    clearValidation('registerEmail');
    clearValidation('firstName');
    clearValidation('lastName');
    clearValidation('registerPassword');
    // Assuming agreeTermsErrorElement exists and clearValidation handles it
    const agreeTermsErrorElement = document.getElementById('agreeTermsError'); 
    if (agreeTermsErrorElement) {
         agreeTermsErrorElement.textContent = '';
    }

    const emailInput = document.getElementById('registerEmail');
    const firstNameInput = document.getElementById('firstName');
    const lastNameInput = document.getElementById('lastName');
    const passwordInput = document.getElementById('registerPassword');
    const agreeTermsInput = document.getElementById('agreeTerms');
    const newsletterInput = document.getElementById('newsletter');

    const email = emailInput.value;
    const firstName = firstNameInput.value;
    const lastName = lastNameInput.value;
    const password = passwordInput.value;
    const agreeTerms = agreeTermsInput.checked;
    const newsletter = newsletterInput.checked;
    
    let isValid = true;
    
    // Validate email
    if (!email) {
        showError('registerEmail', 'Email is required');
        isValid = false;
    } else if (!validateEmail(email)) {
        showError('registerEmail', 'Please enter a valid email');
        isValid = false;
    } else {
        showSuccess('registerEmail');
    }
    
    // Validate first name
    if (!firstName) {
        showError('firstName', 'First name is required');
        isValid = false;
    } else if (firstName.length < 2) {
        showError('firstName', 'First name must be at least 2 characters');
        isValid = false;
    } else {
        showSuccess('firstName');
    }
    
    // Validate last name
    if (!lastName) {
        showError('lastName', 'Last name is required');
        isValid = false;
    } else if (lastName.length < 2) {
        showError('lastName', 'Last name must be at least 2 characters');
        isValid = false;
    } else {
        showSuccess('lastName');
    }
    
    // Validate password
    if (!password) {
        showError('registerPassword', 'Password is required');
        isValid = false;
    } else if (password.length < 8) {
        showError('registerPassword', 'Password must be at least 8 characters.');
        isValid = false;
    } else if (checkPasswordStrength(password) < 4) { // Check for strength good or strong (at least 4 criteria met)
        showError('registerPassword', 'Password is too weak. Use 8+ characters with a mix of letters, numbers & symbols.');
        isValid = false;
    } else {
        showSuccess('registerPassword');
    }
    
    // Validate terms agreement
    if (!agreeTerms) {
        showError('agreeTerms', 'You must agree to the Terms & Conditions and Privacy Policy.');
        isValid = false;
    } else {
        // There's no specific input field for agreeTerms to apply 'is-valid', 
        // and the error message is handled via alert or similar. We'll just clear the error state.
        // For a better UI, a specific error message element next to the checkbox would be ideal.
        // For now, we'll just assume success clears the prior error state if any was set.
        // If you add a dedicated error span for this checkbox, you would modify this line.
        const agreeTermsErrorElement = document.getElementById('agreeTermsError'); // Assuming an error element exists
        if (agreeTermsErrorElement) {
             agreeTermsErrorElement.textContent = '';
        }
    }
    
    if (isValid) {
        const submitBtn = this.querySelector('button[type="submit"]');
        const originalBtnText = submitBtn.textContent;
        submitBtn.textContent = 'Creating account...';
        submitBtn.disabled = true;
          try {
            const response = await fetch('/Identity/Account/RegisterJson', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email, firstName, lastName, password, agreeTerms, newsletter })
            });
            
            const responseData = await response.json();            if (response.ok) {
                // Show success message
                showGeneralSuccess('registerForm', responseData.message || 'Registration successful!');
                
                // Clear the form
                this.reset();
                
                // Redirect to home page or intended URL after a short delay
                setTimeout(() => {
                    window.location.href = responseData.redirectUrl || '/';
                }, 1500);
            }else {
                 if (responseData.errors) {
                    // Display specific errors from the server
                    for (const key in responseData.errors) {
                        if (responseData.errors.hasOwnProperty(key)) {
                            const errorMessages = responseData.errors[key];
                            let inputId = null;
                            switch(key.toLowerCase()) {
                                case 'email':
                                    inputId = 'registerEmail';
                                    break;
                                case 'firstname':
                                    inputId = 'firstName';
                                    break;
                                case 'lastname':
                                    inputId = 'lastName';
                                    break;
                                case 'password':
                                    inputId = 'registerPassword';
                                    break;
                                case 'agreeterms':
                                     inputId = 'agreeTerms';
                                    break;
                                default:
                                    // Handle other potential errors
                                     alert(`Registration failed: ${key} - ${errorMessages.join(' ')}`);
                                    break;
                            }
                           
                            if (inputId) {
                                showError(inputId, errorMessages.join(' '));
                            } else { // Use general error area for non-specific errors
                                showGeneralError('registerForm', errorMessages.join(' '));
                            }
                        }
                    }
                } else if (responseData.message) {
                    // Display general error message
                    showGeneralError('registerForm', responseData.message);
                } else {
                    showGeneralError('registerForm', 'Registration failed. Please try again.');
                }
            }
        } catch (error) {
            console.error('Registration error:', error);
            showGeneralError('registerForm', 'An error occurred during registration. Please try again.');
        } finally {
            submitBtn.textContent = originalBtnText;
            submitBtn.disabled = false;
        }
    }
});

// Forgot password modal
function showForgotPassword() {
    // Clear previous messages when opening modal
    clearGeneralError('forgotPasswordForm');
    clearGeneralSuccess('forgotPasswordForm');
    // Clear previous validation
    clearValidation('forgotEmail');

    new bootstrap.Modal(document.getElementById('forgotPasswordModal')).show();
}

// Forgot password form submission
document.getElementById('forgotPasswordForm').addEventListener('submit', async function(e) {
    e.preventDefault();
    
    // Clear previous errors and success messages
    clearGeneralError('forgotPasswordForm');
    clearGeneralSuccess('forgotPasswordForm');
    clearValidation('forgotEmail');

    const emailInput = document.getElementById('forgotEmail');
    const email = emailInput.value;
    let isValid = true;
    
    if (!email) {
        showError('forgotEmail', 'Email is required');
        isValid = false;
    } else if (!validateEmail(email)) {
        showError('forgotEmail', 'Please enter a valid email');
        isValid = false;
    } else {
        showSuccess('forgotEmail');
    }
    
    if (isValid) {
        const submitBtn = this.querySelector('button[type="submit"]');
        const originalBtnText = submitBtn.textContent;
        submitBtn.textContent = 'Sending...';
        submitBtn.disabled = true;
        
        try {
            const response = await fetch('/Identity/Account/ForgotPassword', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ email })
            });
            
            const responseData = await response.json();

            if (response.ok) {
                // Show success message and potentially close modal
                showGeneralSuccess('forgotPasswordForm', 'Password reset link sent to your email.');
                // Optionally close modal after a delay
                // setTimeout(() => {
                //     bootstrap.Modal.getInstance(document.getElementById('forgotPasswordModal')).hide();
                // }, 3000);
            } else {
                 if (responseData.errors) {
                    // Display specific errors from the server
                    for (const key in responseData.errors) {
                        if (responseData.errors.hasOwnProperty(key)) {
                            const errorMessages = responseData.errors[key];
                            let inputId = null;
                            switch(key.toLowerCase()) {
                                case 'email':
                                    inputId = 'forgotEmail';
                                    break;
                                default:
                                     showGeneralError('forgotPasswordForm', `Password Reset failed: ${key} - ${errorMessages.join(' ')}`);
                                    break;
                            }
                           
                            if (inputId) {
                                showError(inputId, errorMessages.join(' '));
                            } else { // Use general error area for non-specific errors
                                showGeneralError('forgotPasswordForm', errorMessages.join(' '));
                            }
                        }
                    }
                } else if (responseData.message) {
                    // Display general error message
                    showGeneralError('forgotPasswordForm', responseData.message);
                } else {
                    showGeneralError('forgotPasswordForm', 'Password reset failed. Please try again.');
                }
            }
        } catch (error) {
            console.error('Forgot password error:', error);
            showGeneralError('forgotPasswordForm', 'An error occurred during password reset. Please try again.');
        } finally {
            submitBtn.textContent = originalBtnText;
            submitBtn.disabled = false;
        }
    }
});

// Social login handlers - Google OAuth is now handled by forms in the view
// Remove the event listeners that were overriding the Google OAuth buttons

// Keep Facebook coming soon alert
document.querySelectorAll('.btn-facebook').forEach(btn => {
    btn.addEventListener('click', () => {
        alert('Facebook OAuth integration coming soon.');
    });
});
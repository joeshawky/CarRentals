using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Constants
{
    public static class Messages
    {
        public static string CarNotFound { get; set; } = "Car was not found";
        public static string CustomerNotFound { get; set; } = "Customer was not found";
        public static string ImageNotFound { get; set; } = "Image was not found";
        public static string UserNotFound { get; set; } = "User was not found";
        public static string PasswordError { get; set; } = "Wrong password! try again";
        public static string AccessTokenCreated { get; set; } = "Access token was created successfully";
        public static string UserAlreadyExists { get; set; } = "User email address already exists";
        public static string SuccessfulLogin { get; set; } = "Login successful";
        public static string MaxImagesPerCarWasReached { get; set; } = "You can only have 5 images per car";
        public static string UnsupportedImageType { get; set; } = "Unsupported image type. Use png, jpeg or jpg";
        public static string SupportedImageType { get; set; } = "Image type is supported";
        public static string AuthorizationDenied { get; set; } = "Unauthorizes request";
        public static string UserRegistered { get; set; } = "User registered successfully";
        public static string EmailAlreadyExists { get; set; } = "Email already exists";
        public static string BrandNotFound { get; set; } = "Brand does not exist";
        public static string BrandNameAlreadyExists { get; set; } = "Brand name already exists";
        public static string BrandDeleted { get; set; } = "Brand was deleted successfully";
        public static string BrandUpdated { get; set; } = "Brand was updated successfully";
        public static string ImageProcessError { get; set; } = "Image couldn't be processed";
        public static string CarImageAdded { get; set; } = "Car image was added";
        public static string CarImageNotFound { get; set; } = "Car image was not found";
        public static string InaccurateCarImage { get; set; } = "Car image was inaccurate";
        public static string CarImageCreationError { get; set; } = "Car image was not created";
        public static string CarImageUpdated { get; set; } = "Car image was updated";
        public static string ImageDeleted { get; set; } = "Car image was deleted";
        public static string imagecreated { get; set; } = "Image was created";
        public static string ImageProcessed { get; set; } = "Image was processed";
        public static string ColorNotFound { get; set; } = "Color does not exist";
        public static string CarAdded { get; set; } = "Car was added";
        public static string CarDeleted { get; set; } = "Car was deleted";
        public static string InaccurateCar { get; set; } = "Inaccurate car";
        public static string CarUpdated { get; set; } = "Car was updated";
        public static string ColorAdded { get; set; } = "Color was added";
        public static string ColorDeleted { get; set; } = "Color was deleted";
        public static string ColorUpdated { get; set; } = "Color was updated";
        public static string CustomerAdded { get; set; } = "Customer was added";
        public static string CustomerDeleted { get; set; } = "Customer was deleted";
        public static string CustomerUpdated { get; set; } = "Customer was updated";
        public static string RentalAdded { get; set; } = "Rental was added";
        public static string UserAdded { get; set; } = "User was added";
        public static string UserDeleted { get; set; } = "User was deleted";
        public static string UserUpdated { get; set; } = "User was updated";
        public static string UserNotAdded { get; set; } = "User was not added";
        public static string UserOperationClaimAdded { get; set; } = "User operation claim was added";
        public static string UserOperationClaimUpdated { get; set; } = "User operation claim was updated";
        public static string UserOperationClaimDeleted { get; set; } = "User operation claim was deleted";
        public static string UserOperationClaimNotFound { get; set; } = "User operation claim was not found";
        public static string UserOperationClaimInaccurate { get; set; } = "User operation claim was inaccurate";
        public static string OperationClaimAdded { get; set; } = "Operation claim was added";
        public static string OperationClaimUpdated { get; set; } = "Operation claim was updated";
        public static string OperationClaimDeleted { get; set; } = "Operation claim was deleted";
        public static string OperationClaimNotFound { get; set; } = "Operation claim was not found";
        public static string OperationClaimInaccurate { get; set; } = "Operation claim was inaccurate";
        public static string BrandAdded { get; set; } = "Brand was added";
        public static string ColorNameAlreadyExists { get; set; } = "Color name already exists";
        public static string InaccurateRental { get; set; } = "Inaccurate rental info";
        public static string RentalDeleted { get; set; } = "Rental was deleted";
        public static string RentalNotFound { get; set; } = "Rental was not found";
        public static string InvalidCustomer { get; set; } = "Invalid customer";
        public static string CarIsRented { get; set; } = "Car is rented in the wanted date range";
        public static string CarIsRentedWithoutReturnDate{ get; set; } = "Car is unavailable since it's rented with no return date";

    }
}

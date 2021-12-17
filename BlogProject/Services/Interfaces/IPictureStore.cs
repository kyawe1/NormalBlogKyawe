namespace BlogProject.Services.Interfaces;

public interface IPictureStore
{
    byte[]? store(IFormFile file);
}


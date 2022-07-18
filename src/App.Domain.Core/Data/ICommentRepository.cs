namespace App.Domain.Core.Data;

public interface ICommentRepository
{

    #region "Queries"

    Task GetById(int id);
    Task GetAll();

    #endregion



    #region "Commands"

    Task<int> Add();
    Task Update();
    Task Delete();

    #endregion

}
public interface IDataFileService<T>{
    T GetDB();
    void SaveDB(T db);

}

# Lab 01: Repository Pattern

## **ขั้นตอนการทำ Lab**

### **1. สร้าง UserController (คัดลอกจาก StoreController)**
- คัดลอก **StoreController** และเปลี่ยนชื่อไฟล์เป็น **UserController.cs**
- ปรับเปลี่ยนโค้ดภายในให้รองรับ **User** แทน **Store**
- ตรวจสอบการ **Inject Dependency** ให้เหมาะสม

```csharp
public class UserController : ControllerBase
{
    private readonly IUserBAL _UserBAL;

    public UserController(IUserBAL UserBAL)
    {
        _UserBAL = UserBAL;
    }

    [HttpPost("GetUserALL")]
    public IActionResult GetUserALL()
    {
        try
        {
            var UserList = _UserBAL.GetAll(x => x.ActiveFlag == true).ToList();
            if (UserList != null)
            {
                return Ok(new ResponseModel { Message = Messsage.Successfully, Status = APIStatus.Successful, Data = UserList });
            }
            return BadRequest("Posted invalid data.");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
```

---

### **2. สร้าง IUserBAL และ UserBAL (คัดลอกจาก IStoreBAL และ StoreBAL)**
- คัดลอก **IStoreBAL.cs** และ **StoreBAL.cs**
- เปลี่ยนชื่อไฟล์เป็น **IUserBAL.cs** และ **UserBAL.cs**
- แก้ไขให้รองรับ **User** แทน **Store**

**IUserBAL.cs**
```csharp
public interface IUserBAL : IGenericBAL<User>
{
}
```

**UserBAL.cs**
```csharp
public class UserBAL : IUserBAL
{
    private readonly IUserRepository _UserRepository;

    public UserBAL(IUserRepository UserRepository)
    {
        _UserRepository = UserRepository;
    }
}
```

---

### **3. สร้าง IUserRepository และ UserRepository (คัดลอกจาก IStoreRepository และ StoreRepository)**
- คัดลอก **IStoreRepository.cs** และ **StoreRepository.cs**
- เปลี่ยนชื่อไฟล์เป็น **IUserRepository.cs** และ **UserRepository.cs**
- แก้ไขโค้ดให้รองรับ **User** แทน **Store**

**IUserRepository.cs**
```csharp
public interface IUserRepository : IGenericRepository<User>
{
}
```

**UserRepository.cs**
```csharp
public class UserRepository : IUserRepository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DBContext dbContext)
        : base(dbContext)
        {
        }
    }
}
```


**DBContext.cs**
```csharp
modelBuilder.Entity<User>(entity =>
{
    entity.ToTable("User", "dbo");
});
```

---

### **4. สร้าง Model (อย่าลืมใช้ common)**
- สร้างไฟล์ **User.cs** หรือใช้ Model ที่มีอยู่แล้ว
- ตรวจสอบว่ามีการใช้ **Common Namespace** หากจำเป็น

**User.cs**
```csharp
public class User : CommonEntity
{
    [Key]
    public int UserID { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? DateOfBirth { get; set; }
}
```

---

### **5. เพิ่ม Dependency Injection ใน Startup.cs**
- เปิดไฟล์ **Startup.cs** หรือ **Program.cs** (ขึ้นอยู่กับโครงสร้างโปรเจกต์)
- ลงทะเบียน **IUserBAL** และ **IUserRepository** ใน **DI Container**

```csharp
services.AddScoped<IUserBAL, UserBAL>();
services.AddScoped<IUserRepository, UserRepository>();
```

---

## **สรุป**
ใน Lab นี้ คุณได้ทำการ:
✔ คัดลอก **StoreController** และสร้าง **UserController**  
✔ คัดลอก **IStoreBAL และ StoreBAL** เพื่อสร้าง **IUserBAL และ UserBAL**  
✔ คัดลอก **IStoreRepository และ StoreRepository** เพื่อสร้าง **IUserRepository และ UserRepository**  
✔ สร้าง **User Model**  
✔ ลงทะเบียน **Dependency Injection** ใน **Startup.cs**
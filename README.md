# VetCare — Sistema de Gestión de Pacientes Veterinarios
SC-601 · Programación Avanzada · ASP.NET MVC + Entity Framework (Database First)

## Requisitos
- .NET 9 SDK → https://dotnet.microsoft.com/download
- SQL Server (local o Express)
- Git

---

## Configuración en una PC nueva

### 1. Clonar el repositorio
```bash
git clone <URL_DEL_REPO>
cd VetCare
```

### 2. Crear la base de datos
Abra SQL Server Management Studio (SSMS) y ejecute los scripts **en orden**:
1. `Database/01_Crear_VetCareDB.sql` — crea la BD y las tablas
2. `Database/02_Datos_Prueba.sql` — inserta datos de ejemplo

### 3. Ajustar la cadena de conexión
Edite `appsettings.json` con su instancia de SQL Server:
```json
"VetCareDb": "Server=<INSTANCIA>;Database=VetCareDB;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;"
```
Ejemplos de `<INSTANCIA>`:
| Escenario | Valor |
|---|---|
| SQL Server local por defecto | `localhost` |
| SQL Server Express | `localhost\SQLEXPRESS` |
| Instancia con nombre | `COMPUTADORA\SQLSERVER` |

### 4. Restaurar paquetes y ejecutar
```bash
dotnet restore
dotnet run
```
La aplicación abrirá en `http://localhost:5000` (o el puerto que muestre la consola).

---

## Estructura del proyecto
```
VetCare/
├── Controllers/
│   ├── MascotasController.cs   # CRUD + búsqueda LINQ (RF-01, RF-04, RF-05)
│   └── ConsultasController.cs  # Registro + historial (RF-02, RF-03, RF-04)
├── Data/
│   └── VetCareContext.cs       # DbContext — Database First
├── Models/
│   ├── Mascota.cs
│   └── Consulta.cs
├── Views/
│   ├── Mascotas/  (Index, Create, Edit, Delete)
│   └── Consultas/ (Create, Historial)
├── Database/
│   ├── 01_Crear_VetCareDB.sql
│   └── 02_Datos_Prueba.sql
├── appsettings.json
└── Program.cs
```

---

## Publicar en GitHub

```bash
# Desde dentro de la carpeta VetCare/
git init
git add .
git commit -m "Initial commit: VetCare MVC + EF Database First"
git remote add origin https://github.com/<TU_USUARIO>/<TU_REPO>.git
git push -u origin main
```

---

## Requerimientos cubiertos
| RF | Descripción | Implementación |
|---|---|---|
| RF-01 | CRUD de mascotas | `MascotasController` — Index/Create/Edit/Delete |
| RF-02 | Registro de consultas | `ConsultasController.Create` |
| RF-03 | Historial médico ordenado desc | `ConsultasController.Historial` |
| RF-04 | Validaciones y reglas de negocio | Anotaciones + lógica en controladores |
| RF-05 | Búsqueda LINQ por nombre/especie | `MascotasController.Index(search)` |

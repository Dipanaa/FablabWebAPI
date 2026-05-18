# ⚙️ FabLab INACAP Maipú - BackEnd (Web API)

Bienvenido al repositorio central de lógica del Sistema FabLab INACAP Maipú. Esta **Web API RESTful** actúa como el núcleo tecnológico transaccional, orquestando el acceso a bases de datos, validando la seguridad y alimentando de información a todas las interfaces (Landing Page, Intranet Web y Aplicación Móvil).

## 🛠️ Stack Tecnológico
* **Framework:** ASP.NET Core 9 (C#)
* **ORM:** Entity Framework Core (EFC)
* **Base de Datos:** Microsoft SQL Server
* **Cloud & Infraestructura:** Microsoft Azure Cloud (App Services, SQL Database, Storage Accounts)
* **Seguridad y Accesos:** Autenticación JWT
* **Servicios IA:** Gemini 2.5 

## 📐 Arquitectura y Patrones de Diseño
* **Arquitectura N-Capas (N-Tier):** Separación estricta y unidireccional de responsabilidades, garantizando un código mantenible y altamente escalable:
  * **Capa de Presentación / Controladores:** Endpoints HTTP encargados de recibir peticiones y devolver respuestas al cliente de manera unificada.
  * **Capa de Negocio:** Lógica de validación, algoritmos y reglas propias del entorno del laboratorio.
  * **Capa de Datos:** Implementada con contextos EFC y repositorios, centralizando todas las operaciones a la base de datos SQL.
* **Patrón MVC Interno:** Uso nativo del marco ASP.NET Core para clasificar fluidamente el enrutamiento y las acciones a ejecutar.
* **Inyección de Dependencias (DI):** Principio aplicado desde el contenedor base asegurando que los Controladores no acoplen directamente instanciaciones.
* **Patrón DTO (Data Transfer Objects):** Vital para la seguridad y optimización del tráfico web. Los mapeos aseguran que únicamente se transmita la información requerida al FrontEnd, ocultando metadatos sensibles o contraseñas.
* **Stateless API:** Sin almacenamiento local de sesiones. Cada petición se valida a través de su JWT correspondiente, asegurando disponibilidad para implementaciones de balanceo de carga futuro.

## ⚙️ Principales Implementaciones
* **Seguridad Avanzada, Encriptación y Hashing:** Implementación de cifrado con protección contra ataques de tabla arcoíris (Hashing + Salting integrados en la lógica de registro). Además, todo tráfico es encriptado en la capa de transporte usando TLS (HTTPS).
* **Consultas Optimizadas vía LINQ:** Manejo eficiente de transacciones con la base de datos relacional de Azure, mitigando íntegramente las vulnerabilidades de *Inyección SQL*.
* **Endpoints Asíncronos (async/Task):** Cada controlador hace uso de hilos asíncronos para evitar bloqueos del servidor y cumplir estrictamente el SLA de tiempos de respuesta menores a 600ms por petición.
* **Relacionalidad Robusta:** Administración transaccional completa para múltiples entidades (`Usuarios`, `Roles`, `Proyectos`, `HitosProyecto`, `Noticias`, `Inventario` y `FormulariosIngreso`).
* **Cloud-Ready:** Ecosistema integrado sobre una red virtual de Azure, preparado para la interconexión con Azure Storage para resguardo de imágenes.

## 📋 Calidad, Ética y Cumplimiento
* **Cumplimiento Legal y Ético:** Resguardo alineado con la **Ley 21.719 (Protección de la Vida Privada)** para el tratamiento y confidencialidad de los datos personales en Chile.
* **Adhesión a Estándares ISO:** Estructura planificada y orientada a los atributos de calidad definidos por la norma **ISO/IEC 25010** (Usabilidad, Fiabilidad, Mantenibilidad, Seguridad) y respaldada mediante casos de prueba rigurosos de validación (Postman).

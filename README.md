# Descripcion del proyecto
Algoritmo de planificación FCFS (First Come First Server) Continiacion

# Especificaciones
1. En esta práctica se contempla el diagrama de Cinco Estados:
    a. Nuevo: Procesos que se acaban de crear pero aún no han sido admitidos por el sistema
       operativo en el grupo de procesos ejecutables.
    b. Listos: Procesos que están preparados para ejecutarse, en cuanto sea su turno.
    c. Ejecución: Proceso que está actualmente en ejecución.
    d. Bloqueado: Proceso que no puede ejecutar hasta que se produzca cierto suceso, como
       la terminación de una operación de E/S.
    e. Terminado: Un proceso que ha sido excluido por el sistema operativo del grupo de
       procesos activos, bien porque se detuvo o porque fue abandonado por alguna razón.
2. El algoritmo por implementar para la cola de listos es el FCFS es decir los procesos se
   ordenarán conforme van llegando.
3. Se preguntará el Número de Procesos inicial pudiendo ser N procesos.
4. El máximo de procesos en memoria será de 5 (Recuerde que los estados a considerar en este
   caso son: Ejecución, Bloqueado y Listo).
5. Los procesos que no tengan cupo en la cola de listos deberán quedarse en la cola de Nuevos,
   esperando a que algún proceso termine y así ingresar al sistema.
6. A cada uno de los procesos que se generan (punto 3) se le asignará internamente:
    a. Identificador de proceso único. (Puede ser secuencial)
    b. Tiempo máximo estimado (aleatorio, validar que sea mayor a 0 y en un rango entre
       6 y 15).
    c. Datos para la operación a realizar (aleatorio, validar error aritmético). (Ver punto 2
       de la actividad 4 (programa 2))
7. Las teclas a utilizar son: 
<img src="https://cdn.discordapp.com/attachments/762088441314934794/859559833291456512/unknown.png" width="706" height="616">
# Contacto
christian.soto4752@alumnos.udg.mx  

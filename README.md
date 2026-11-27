Nómina Quincenal

Aplicación de escritorio en C# con Windows Forms que calcula el pago quincenal de empleados, valida los datos ingresados y muestra un resumen con los totales de la nómina.

Proyecto de la actividad individual de Programación Básica.

Autor: Ruben Dario Familia Febles
Institución: Universidad Central del Este (UCE)
Lenguaje / tecnología: C# · Windows Forms (.NET)
Funcionalidades
Registro de empleados con nombre, tipo de contrato (Fijo o Temporal), cargo, horas trabajadas e incentivo de transporte.
Cálculo automático del pago neto de cada empleado.
Lista de la nómina registrada ().ListBox
Resumen con la cantidad de empleados y los totales de bruto, deducciones y neto, en formato .RD$ 0.00
Validación de datos con mensajes claros ().MessageBox
Botón para limpiar el formulario y volver al estado inicial.
Reglas de cálculo

Tarifas por hora (RD$):

Cargo	Tarifa
Auxiliar	180
Técnico	250
Supervisor	350
Gerente	500

Fórmulas:

Salario regular = horas (hasta 88) × tarifa
Horas extra (las que pasen de 88) = horas extra × tarifa × 1.35
Incentivo de transporte = RD$ 1,500 si el está marcadoCheckBox
Bruto = salario regular + horas extra + incentivo
Deducciones
Contrato Fijo: AFP 2.87 % + SFS 3.04 % sobre (regular + extra), sin incluir el incentivo
Contrato Temporal: sin deducciones
Neto = bruto − deducciones
Validaciones
Campo	Regla
Nombre	No puede estar vacío
Contrato	Debe seleccionarse Fijo o Temporal
Cargo	Debe seleccionarse un cargo del ComboBox
Horas	Número entero entre 1 y 120 (int.TryParse)
Resumen	No se muestra si no hay empleados registrados
Controles del formulario
Control	Nombre
Nombre del empleado	txtEmpleado
Contrato	rdbFijo, rdbTemporal
Cargo	cmbCargo
Horas trabajadas	txtHoras
Incentivo de transporte	chkTransporte
Registrar empleado	btnRegistrar
Nómina registrada	lstNomina
Resumen	lblEmpleados, , , lblBrutolblDeduccioneslblNeto
Mostrar resumen	btnResumen
Limpiar	btnLimpiar
Salir	btnSalir
Cómo ejecutarlo
Abre la solución () en Visual Studio..sln
Presiona F5 (o Iniciar) para compilar y ejecutar.
Completa los datos del empleado y presiona Registrar empleado.
Repite para cada empleado y presiona Mostrar resumen para ver los totales.
Estructura del proyecto

Todo el código está en , sin clases adicionales:Form1.cs

Constantes: horas regulares (88), recargo de horas extra (1.35), incentivo (1,500), tasas AFP y SFS, límites de horas y tarifas por cargo.
Acumuladores: variables a nivel de formulario (cantidad de empleados, total bruto, total deducciones y total neto).
Eventos: , , , y .Form1_LoadbtnRegistrar_ClickbtnResumen_ClickbtnLimpiar_ClickbtnSalir_Click
Método auxiliar: , que limpia los campos de entrada.LimpiarCampos()
Restricciones cumplidas
Solo controles básicos: sin , bases de datos, archivos ni librerías externas.DataGridView
Sin valores fijos repetidos en el código (todo en constantes) y sin .Console
Cada evento está comentado.

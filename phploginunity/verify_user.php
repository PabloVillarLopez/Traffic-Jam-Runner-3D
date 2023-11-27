<?php
$servidor = "localhost";
$bbdd = "bbddlogin";
$usuario = "root";
$contrasena = "";

try {
	$conn = mysqli_connect($servidor, $usuario, $contrasena, $bbdd);

	if (!$conn) 
	{
		echo "'{Codigo: 400', 'Mensaje': 'Error intentando conectar', 'Respuesta': ''}";
	}
	else 
	{
		echo "'{Codigo: 200', 'Mensaje': 'Conectado correctamente', 'Respuesta': ''}";

		if (isset($_GET['usuario']))  
		{
			$usuario = $_GET['usuario'];

			$sql = "SELECT * FROM `usuarios` WHERE usuario ='".$usuario."';";
			$resultado = $conn->query($sql);

			if ($resultado->num_rows > 0) 
			{
				echo "{'codigo':202, 'mensaje': 'El usuario existe en el sistema', 'respuesta':'".$resultado->num_rows."'}";
			}
			else 
			{
				echo "{'codigo':203, 'mensaje': 'El usuario NO existe en el sistema', 'respuesta': '0'}";
			}
		}
		else 
		{
			echo "{'codigo':402, 'mensaje': 'Faltan datos para ejecutar la accion solicitada', 'respuesta': ''}";
		}

		
		

		
	}
} catch (Exception $e) {
	echo "
		'{Codigo: 400', 'Mensaje': 'Error intentando conectar', 'Respuesta': ''
		}";
}


$conn->close();
?>
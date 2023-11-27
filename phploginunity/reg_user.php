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
		echo "'{Codigo: 200', 'Mensaje': 'Conectado correctamente', 'Respuesta': ''
		}";

		if (isset($_POST['usuario']) && 
			isset($_POST['contrasena']))  
		{
			$usuario = $_POST['usuario'];
			$contrasena = $_POST['contrasena'];

			$sql = "SELECT * FROM `usuarios` WHERE usuario ='".$usuario."';";
			$resultado = $conn->query($sql);

			if ($resultado->num_rows > 0) 
			{
				echo "{'codigo':403, 'mensaje': 'Ya existe un usuario registrado con ese nombre', 'respuesta':'".$resultado->num_rows."'}";
			}
			else 
			{

				$sql = "INSERT INTO `usuarios` (`id`, `usuario`, `contrasena`, `superusuario`) VALUES (NULL, '".$usuario."', '".$contrasena."', '0');";

				if ($conn->query($sql) === TRUE) 
				{
					$sql = "SELECT * FROM `usuarios` WHERE usuario ='".$usuario."';";
					$resultado = $conn->query($sql);
					$texto = '';

					while ($row = $resultado->fetch_assoc()) {
						$texto = 
						"{#id#:".$row['id'].
						", #usuario:#".$row['usuario'].
						"#,#contrasena#:#".$row['contrasena']."}";
					}

					echo "{'codigo':201, 'mensaje': 'Usuario creado correctamente', 'respuesta': '".$texto."'}";
				}
				else 
				{
					echo "{'codigo':401, 'mensaje': 'Error intentando crear el usuario', 'respuesta': ''}";
				}
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
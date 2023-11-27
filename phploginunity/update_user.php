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
		//echo "'{Codigo: 200', 'Mensaje': 'Conectado correctamente', 'Respuesta': ''}";

		if (isset($_POST['usuario']) &&
			isset($_POST['puntuacion']) &&
			isset($_POST['contrasena']))  
		{
			$usuario = $_POST['usuario'];
			$puntuacion = $_POST['puntuacion'];
			$contrasena = $_POST['contrasena'];

			$sql = "SELECT * FROM `usuarios` WHERE usuario ='".$usuario."';";
			$resultado = $conn->query($sql);

			if ($resultado->num_rows > 0) 
			{
				$sql = "UPDATE `usuarios` SET `puntuacion` = '".$puntuacion."' WHERE usuario = '".$usuario."';";
				$conn->query($sql);

				$sql = "SELECT * FROM `usuarios` WHERE usuario ='".$usuario."';";
				$resultado = $conn->query($sql);
				$texto = '';

				while ($row = $resultado->fetch_assoc()) {
					$texto = 
					"{#id#:".$row['id'].
					",#usuario#:#".$row['usuario'].
					"#,#contrasena#:#".$row['contrasena'].
					"#,#puntuacion#:".$row['puntuacion'].
					"}";
				}

				echo '{"codigo":207, "mensaje": "Puntuacion actualizada con exito", "respuesta":"'.$texto.'"}';
			}
			else 
			{
				echo '{"codigo":208, "mensaje": "Puntuacion no actualizada", "respuesta": ""}';
			}
		}
		else 
		{
			echo '{"codigo":402, "mensaje": "Faltan datos para ejecutar la accion solicitada", "respuesta": ""}';
		}

		
		

		
	}
} catch (Exception $e) {
	echo "
		'{Codigo: 400', 'Mensaje': 'Error intentando conectar', 'Respuesta': ''
		}";
}


$conn->close();
?>
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
			isset($_POST['contrasena']) &&
			isset($_POST['contrasena2']))  
		{
			$usuario = $_POST['usuario'];
			$contrasena = $_POST['contrasena'];
			$contrasena2 = $_POST['contrasena2'];

			$sql = "SELECT * FROM `usuarios` WHERE usuario ='".$usuario."' and contrasena='".$contrasena."';";
			$resultado = $conn->query($sql);

			if ($resultado->num_rows > 0) 
			{
				$sql = "UPDATE `usuarios` SET `contrasena` = '".$contrasena2."' WHERE usuario = '".$usuario."';";
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

				echo '{"codigo":206, "mensaje": "Usuario editado con exito", "respuesta":"'.$texto.'"}';
			}
			else 
			{
				echo '{"codigo":204, "mensaje": "El usuario o la contrasena son incorrectos", "respuesta": ""}';
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
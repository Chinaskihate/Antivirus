Программа параллельно сканирует файлы на подозрительные строки.
Есть серверная часть(API) и клиентская(консольное приложение).

Для запуска серверной части необходимо пройти в Antivirus.API\bin\debug\net6.0, далее написать .\Antivirus.API.
(При запуске из Visual Studio необходимо сменить IIS Express на API).

Для запуска клиентской части необходимо пройти в Antivirus.CMD\bin\debug\net6.0, далее написать .\Antivirus.CMD <путь к директории>

Команды в клиентской части:
	scan <path to directory>:
		Возвращает ответ в виде:
		Scan was created with ID: <id>

	status <scan id>:
		Возвращает ответ в виде:

		====== SCAN STATUS ======
    		FINISHED: <is finished>
    		PROCESSED FILES: <total processed files>
    		JS DETECTS: <total evil js detects>
    		RM -RF DETECTS: <total rm -rf detects>
    		RUNDLL32 DETECTS: <total runDLL detects>
    		NUMBER OF ERRORS: <number of errors>
    		EXECUTION TIME: <execution time>
    		ERRORS:
			<List of occured errors>
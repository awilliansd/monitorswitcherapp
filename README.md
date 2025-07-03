# MonitorSwitcherApp

Um pequeno utilitário em C# que permite alternar rapidamente o monitor principal entre dois monitores, usando um ícone na bandeja do sistema.

Ideal para quem utiliza dois monitores e precisa alternar entre:

- 🎮 **Modo Jogo**: monitor menor como principal (onde os jogos da Steam abrem por padrão).
- 🧑‍💻 **Modo Reunião/Trabalho**: monitor maior como principal (evita que notificações interrompam apresentações).

---

## 📸 Captura de tela

> Um ícone na bandeja do Windows permite alternar entre os modos com um clique.

![MonitorSwitcher Tray](docs/monitor-switcher-tray.png)

---

## ⚙️ Requisitos

- .NET 6 ou superior (sugestão: .NET 8).
- Windows 10/11.
- [MultiMonitorTool](https://www.nirsoft.net/utils/multi_monitor_tool.html) da NirSoft (utilitário portátil, usado para alternar o monitor principal).

---

## 🚀 Como usar

### 1. Baixe o `MultiMonitorTool`
- Site oficial: https://www.nirsoft.net/utils/multi_monitor_tool.html
- Extraia o `MultiMonitorTool.exe` na mesma pasta do `MonitorSwitcherApp.exe`.

### 2. Configure o arquivo `display_config.txt`
Este arquivo mapeia os nomes dos monitores identificados pelo Windows:

```
DISPLAY1=\\.\DISPLAY1
DISPLAY2=\\.\DISPLAY2
```

> Você pode descobrir os nomes exatos usando o próprio MultiMonitorTool (ao abrir, ele exibe `\\.\DISPLAY1`, `\\.\DISPLAY2`, etc.).

### 3. Execute o app
- Rode o `MonitorSwitcherApp.exe`.
- Ele ficará na **bandeja do sistema** com duas opções:
  - `Modo Jogo` → define o DISPLAY1 como principal.
  - `Modo Reunião` → define o DISPLAY2 como principal.

---

## 🛠️ Compilando a partir do código

### 1. Clonar o repositório
```bash
git clone https://github.com/seu-usuario/MonitorSwitcherApp.git
cd MonitorSwitcherApp
```

### 2. Restaurar e compilar (usando .NET SDK)
```bash
dotnet build
```

> Certifique-se de estar usando `.NET 6`, `.NET 7` ou `.NET 8`.  
> Caso veja o aviso `NETSDK1137`, pode ignorar — ele é apenas informativo.

---

## 📦 Estrutura do projeto

```
MonitorSwitcherApp/
├─ Program.cs
├─ MonitorSwitcherApp.csproj
├─ display_config.txt
├─ MultiMonitorTool.exe  ← (adicione manualmente)
```

---

## 🔒 Sobre o MultiMonitorTool

O `MultiMonitorTool.exe` é um utilitário portátil e seguro da NirSoft. Este projeto apenas o **chama via linha de comando**, sem modificações.

---

## 📝 Licença

Este projeto é de código aberto sob a licença [MIT](LICENSE).  
Você é livre para modificar, distribuir e usar conforme desejar.

---

## 🙋‍♂️ Autor

Desenvolvido por [Alessandro Dias](https://github.com/awilliansd).  
Contribuições, melhorias e sugestões são bem-vindas!

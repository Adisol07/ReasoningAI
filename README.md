# Reasoning AI
 > Open Source alternative to OpenAI o1 reasoning model.
---
 > [!WARNING]
 > Keep in mind that it is still in beta and therefore you may encounter bugs!
---
![Screenshot](https://raw.githubusercontent.com/Adisol07/ReasoningAI/main/strawberry_example.png)

## Installation
First you need to download & install Ollama.\
Then download the ReasoningModel.txt file from the latest release and add the ReasoningAI model to ollama with this command: ```ollama create ReasoningAI -f file_path_to_ReasoningModel.txt``` \
Then just simply download the ReasoningAI_your_os.zip and extract it.\
When that is done just run the ReasoningAI program and enjoy!

## Future goals
 - [ ] Chat - currently the model does not have a chat feature - one prompt, one response.
 - [ ] Saving chats and loading them in the app.
 - [ ] API - add the ability to open API.
 - [-] Final model response - currently the model seems like it is answering to itself - which is partially a feature but the final response should be targetted to you and not the AI model.
 - [ ] Thinking title delay removal - right now the thinking text is one response off.
 - [ ] Config location - the app now creates and looks for config.json file in the directory you are currently in which can lead to problems.
 - [ ] Thinking title location fix

## Details
---
 > [!CAUTION]
 > You can modify everything about the model in its model file but keep in mind that some behaviour of the model is required for the program to work
---
Base model: llama 3.1\
Version: 1.1.0 - beta\
Context size: 4096

## License
This app inherits license rules of ollama and llama3.1 model because the app uses them.\
You are allowed to use, distribute or modify the code and/or the model file.\
Credits are not required but appreciated :)

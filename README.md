# Description
This is a simple form that generates texts to be sent to all clietns.  
Message will be displayed before player entering Land/Room selection screen.  


# Download
GO release section and download latest version.

# Setup
Replace your `server\signserver\dsgn_resp.go` with new `mhf_text_generator\server\signserver\dsgn_resp.go` in downloaeded file.  
Be sure not to create a backup file inside of server folder otherwise it won't launch.

# Usage
Run `mhf_text_generator.exe` and edit texts as you wish.  
Press `Generate`and you'll get string of bytes.
Copy and paste it to `dsgn_resp.go`, line 90.

# Known issue
- Sometimes it doesn't change color and text size.
- Doesn't support Japanese(currently).

# Images
## In editor
![image](https://user-images.githubusercontent.com/89909040/164960406-2c641c49-9208-4274-9faa-35d347ae8870.png)

## In game
![image](https://user-images.githubusercontent.com/89909040/164960428-1c162fa4-d37f-4015-b995-b22b2c023cb3.png)

# Changelog
## 1.0
Initial release.

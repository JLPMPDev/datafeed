# Datafeed
Datafeed ...

## Installation

## Configuration
Datafeed is driven by a configuration file (`config.yaml`) which is placed
in the same directory as Datafeed's executable.

``` yaml
email:
  - name: Nicholas Bayley
    address: nick_bayley@johnlewis.co.uk
template:
  path: C:\Temp\template.html
feed:
  path: C:\Temp\datafeed.txt
report:
  title: OCCO Check
```

| Configuration | Description | Mandatory |
| ------------- | ----------- | --------- |
| `email: name` | Name of the email recipient | &#10004; |
| `email: address` | Address of the email recipient | &#10004; |
||||
| `template: path` | Absolute or relative path to the HTML template (default: `template.html`) | &#10008; |
||||
| `feed: path` | Absolute or relative path to the feed to be parsed (default: `feed.txt`) | &#10008; |
||||
| `report: title` | The reports title. This is used in the template (`reportTitle`) | &#10008; |
||||
| `smtp: host` | Host name or IP of the SMTP server | &#10004; |
| `smtp: port` | Port of the SMTP server (default: `25`) | &#10008; |
## Feed

## Template

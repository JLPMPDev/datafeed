# Datafeed
Datafeed ... :construction:

## Installation
:construction:

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
| `email: name` | Name of the email recipient | ✘ |
| `email: address` | Address of the email recipient | ✔ |
||||
| `template: path` | Absolute or relative path to the HTML template (default: `template.html`) | ✘ |
||||
| `feed: path` | Absolute or relative path to the feed to be parsed (default: `feed.txt`) | ✘ |
||||
| `report: title` | The reports title, this is used in the template (variable: `reportTitle`, default: `Datafeed`) | ✘ |
||||
| `smtp: host` | Host name or IP of the SMTP server | ✔ |
| `smtp: port` | Port of the SMTP server (default: `25`) | ✘ |

## Feed
:construction:

## Template
:construction:

## Scheduler
:construction:

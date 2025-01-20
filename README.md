# Terraform Plan Guide

This guide will walk you through the steps to run `terraform plan` in your local environment. `terraform plan` is used to generate and show an execution plan for Terraform, which describes the changes Terraform will make to your infrastructure.

## Prerequisites

Before running `terraform plan`, ensure that you have the following installed and set up:

- **Terraform**: [Download Terraform](https://www.terraform.io/downloads.html) (v1.5.2 or higher recommended)
- **AWS CLI**: [Install AWS CLI](https://docs.aws.amazon.com/cli/latest/userguide/install-cliv2.html)
- **AWS Credentials**: Ensure your AWS credentials are configured. You can set this up by running `aws configure` or by creating the `~/.aws/credentials` file.

### Example AWS credentials setup:
```ini
[default]
aws_access_key_id = YOUR_ACCESS_KEY
aws_secret_access_key = YOUR_SECRET_KEY
region = us-east-1


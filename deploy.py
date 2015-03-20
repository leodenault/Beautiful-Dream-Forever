#!/usr/bin/python
from subprocess import Popen, PIPE
from decimal import getcontext, Decimal

SOURCE_REPO = "git@github.com:leodenault/fashion.git"
STAGING_REPO = "git@heroku.com:fashion-game-staging.git"

def execute_command(command, *args):
	command_list = [command]
	command_list.extend(args)

	process = Popen(command_list, stderr=PIPE, stdout=PIPE)
	output, err = process.communicate()
	code = process.returncode
	
	if (code == 0):
		return output.decode("utf-8")
	else:
		raise RuntimeError(str.format("An error occurred while executing command {command}:\n\n {err}",
			command=command_list[0], err=err.decode("utf-8")))

def get_git_branch():
	branch_output = execute_command("git", "branch")
	asterisk_index = branch_output.find("*")
	newline_index = branch_output.find("\n", asterisk_index)
	branch = branch_output[asterisk_index+2:newline_index]
	return branch

# Get the previous tag number from Git
tag_info = execute_command("git", "describe")
dash_index = tag_info.find("-")
previous_tag = tag_info[1:dash_index]
print(str.format("Retrieved previous tag {tag} from Git", tag=tag_info[0:dash_index]))

# Get major and minor versions
decimal_index = previous_tag.find(".")
major = previous_tag[:decimal_index]
minor = previous_tag[decimal_index+1:]

# Create the new tag number
minor = int(minor) + 1
next_tag = str.format("v{major}.{minor}", major=major, minor=minor)


#Get the tag message from the user
tag_message = ""

while tag_message == "":
	tag_message = raw_input("Please enter a tag message: ")

branch = get_git_branch();
print(str.format("Detecting that Git is currently on {branch} branch", branch=branch))


# Create the tag in Git
execute_command("git", "tag", "-a", next_tag, "-m", tag_message)
print(str.format("Created new tag {tag}", tag=next_tag))

# Push the tag to Github
execute_command("git", "push", SOURCE_REPO, str.format("{branch}:staging", branch=branch), "--tags")
print(str.format("Pushed tag {tag} to {repo}", tag=next_tag, repo=SOURCE_REPO))

# Push the tag and commit to Heroku staging
print(str.format("Pushing to staging server {repo}", repo=STAGING_REPO))
execute_command("git", "push", STAGING_REPO, str.format("{branch}:master", branch=branch), "--tags")
print(str.format("Pushed tag {tag} to {repo}", tag=next_tag, repo=STAGING_REPO))


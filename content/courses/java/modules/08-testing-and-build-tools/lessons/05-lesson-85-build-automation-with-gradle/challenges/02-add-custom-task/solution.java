tasks.register("projectInfo") {
    group = "help"
    description = "Displays project information"
    
    doLast {
        println("================================")
        println("Project Information")
        println("================================")
        println("Name: ${project.name}")
        println("Version: ${project.version}")
        println("Java Version: ${java.toolchain.languageVersion.get()}")
        println("================================")
    }
}
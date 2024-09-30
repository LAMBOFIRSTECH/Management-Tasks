/* groovylint-disable-next-line CompileStatic */
pipeline {
    agent { label 'Linux' }
    environment {
        WORKSPACE_DIR = "${env.WORKSPACE}"
        APP_DIR = "${WORKSPACE_DIR}/Management_Tasks"
        scannerHome = tool 'sonarscanner'
        PROJECT_KEY = 'Sonar-webConsulTasks'
        PROJECT_NAME = 'TasksManagement-APP'
        PROJECT_VERSION = '1.0'
        SONAR_SCANNER_PATH = "${scannerHome}/sonar-scanner-5.0.1.3006/bin/sonar-scanner"
        SONAR_LANGUAGE = 'cs'
        SONAR_ENCODING = 'UTF-8'
        COVERAGE_PATH = "${WORKSPACE_DIR}/TestResults"
        OPENCOVER_REPORT_PATH = "${COVERAGE_PATH}/coverage.cobertura.xml"
        VSTEST_REPORT_PATH = "${COVERAGE_PATH}/*.trx"
        DOCKER_HUB_CREDENTIALS = ''
    }

    stages {
        stage('Clonage du référentiel GitHub') {
            steps {
                checkout scm
            }
        }

        stage('Pré-traitement') {
            steps {
                /* groovylint-disable-next-line GStringExpressionWithinString */
                sh '''
                    rm  *.md
                '''
            }
        }
        // stage('Build') {
        //     steps {
        //         // Si vous avez des scripts de build dans package.json
        //         bat 'npm run build'
        //     }
        // }

        // stage('Build') {
        //     steps {

        //     }
        // }
        // stage('Déploiement dans un container Docker') {
        //     steps {
        //         script {

        //         }
        //     }
        // }
        stage('Build de l\'image docker') {
            steps {
                sh '''
                   docker build -t web-app .
                   '''
            }
        }
        stage('Déploiement dans un container Docker') {
            steps {
                script {
                    /* groovylint-disable-next-line NestedBlockDepth */
                    try {
                        /* groovylint-disable-next-line GStringExpressionWithinString, LineLength */
                        sh 'docker run -d -p 5195:5195 -p 7251:7251 --name ${PROJECT_NAME} -v ${COVERAGE_PATH}:/TestResults  web-app'

                        sleep(time: 10, unit: 'SECONDS')

                        /* groovylint-disable-next-line GStringExpressionWithinString */
                        sh 'docker exec ${PROJECT_NAME} /bin/bash -c "chmod -R 777 /TestResults"'
                    /* groovylint-disable-next-line CatchException, NestedBlockDepth */
                    } catch (Exception e) {
                        currentBuild.result = 'FAILURE'
                        throw e
                    }
                }
            }
        }
        stage('Vérification via SonarQube ') {
            steps {
                script {
                    /* groovylint-disable-next-line GStringExpressionWithinString */
                    sh '''
                        if ! find ${COVERAGE_PATH} -type f -name 'coverage.cobertura.xml'; then
                            echo "Le rapport analytique introuvable*****************"
                            exit 1
                        fi
                    '''
                    /* groovylint-disable-next-line NestedBlockDepth */
                    withSonarQubeEnv('SonarQube-Server') {
                        sh """
                            ${SONAR_SCANNER_PATH} \
                            -Dsonar.projectKey=${PROJECT_KEY} \
                            -Dsonar.projectName="${PROJECT_NAME}" \
                            -Dsonar.projectVersion=${PROJECT_VERSION} \
                            -Dsonar.sources=${API_DIR} \
                            -Dsonar.language=${SONAR_LANGUAGE} \
                            -Dsonar.sourceEncoding=${SONAR_ENCODING} \
                            -Dsonar.cs.opencover.reportsPaths=${OPENCOVER_REPORT_PATH} \
                            -Dsonar.cs.vstest.reportsPaths=${VSTEST_REPORT_PATH}
                        """
                    }
                }
            }
        }
    }

    post {
        success {
            echo 'Le pipeline s\'est exécuté avec succès!'
            sh 'rm -f Jenkinsfile'
        }
        failure {
            echo 'Le pipeline a échoué!'
        }
    }
// Rajouter la stack trivy comme étape pour vérifier l'image docker
}

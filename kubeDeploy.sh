cd KubernetesDeploy
kubectl delete -f Namespace.yaml
kubectl apply -f Namespace.yaml
kubectl apply -f Services.yaml
kubectl apply -f Deployments.yaml
kubectl apply -f ScaledWorker.yaml
cd ..
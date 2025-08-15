export default function notify(type, message, title = '') {
    const notification = document.createElement('div');
    notification.classList.add('notification', type);

    notification.innerHTML = `
        ${title ? `<strong style="display:block; margin-bottom: 5px;">${title}</strong>` : ''}
        <span>${message}</span>
    `;

    notification.style.position = 'fixed';
    notification.style.top = '20px';
    notification.style.right = '20px';
    notification.style.padding = '10px 20px';
    notification.style.borderRadius = '5px';
    notification.style.color = '#fff';
    notification.style.fontSize = '14px';
    notification.style.zIndex = 9999;
    notification.style.backgroundColor =
        type === 'success' ? 'green' :
            type === 'warning' ? 'orange' :
                'red';
    notification.style.boxShadow = '0 2px 6px rgba(0, 0, 0, 0.2)';
    notification.style.maxWidth = '300px';
    notification.style.wordWrap = 'break-word';

    document.body.appendChild(notification);

    setTimeout(() => {
        notification.remove();
    }, 3000);
}
